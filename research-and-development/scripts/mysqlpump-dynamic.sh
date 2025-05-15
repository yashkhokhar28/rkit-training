#!/bin/bash

# Configuration Section

# Path to the log file where all backup/restore operations and statuses will be recorded.
LOG_FILE="/var/log/mysql_backup_restore.log"

# Directory where all backups (full and incremental) will be stored.
BACKUP_DIR="/home/ubuntu/backups"

# MySQL user with privileges to perform dump and restore operations.
MYSQL_USER="root"


# Color Output Definitions

# ANSI escape codes for coloring terminal output — used to enhance log readability when printing to console.
COLOR_RESET='\033[0m'     # Resets color to default
COLOR_GREEN='\033[32m'    # Green for success/completion messages
COLOR_BLUE='\033[34m'     # Blue for general process messages (e.g., backup, restore steps)
COLOR_YELLOW='\033[33m'   # Yellow (not used yet — can be used for warnings)
COLOR_RED='\033[31m'      # Red for errors or failures
COLOR_CYAN='\033[36m'     # Cyan for neutral information or other messages


# Function: log_message
# Logs a message with timestamp to both log file and colored console output.
# Coloring is determined based on keywords in the message to make output easier to read in terminal.

log_message() {
    TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")  # Get current timestamp
    MESSAGE="$1"                            # The message to log

    # Log to file
    echo "$TIMESTAMP - $MESSAGE" >> "$LOG_FILE"

    # Determine color based on message content and print to terminal
    if [[ "$MESSAGE" == *"error"* || "$MESSAGE" == *"fail"* ]]; then
        echo -e "${COLOR_RED}$TIMESTAMP - $MESSAGE${COLOR_RESET}"      # Red for errors/failures
    elif [[ "$MESSAGE" == *"completed"* || "$MESSAGE" == *"success"* ]]; then
        echo -e "${COLOR_GREEN}$TIMESTAMP - $MESSAGE${COLOR_RESET}"    # Green for completions/success
    elif [[ "$MESSAGE" == *"backup"* || "$MESSAGE" == *"insert"* || "$MESSAGE" == *"restore"* ]]; then
        echo -e "${COLOR_BLUE}$TIMESTAMP - $MESSAGE${COLOR_RESET}"     # Blue for backup/restore steps
    else
        echo -e "${COLOR_CYAN}$TIMESTAMP - $MESSAGE${COLOR_RESET}"     # Cyan for all other info
    fi
}


# Function: get_master_status
# Retrieves the current binary log file and position from the MySQL server.
# This information is essential for setting the start point for incremental backups.

get_master_status() {
    mysql -u $MYSQL_USER -e "SHOW MASTER STATUS\G" | awk '
        /File:/ { file=$2 }           # Extract the binlog file name (e.g., mysql-bin.000001)
        /Position:/ { pos=$2 }        # Extract the current binlog position (e.g., 12345)
        END {
            if (file && pos) {
                print file, pos       # Print file and position as output (space-separated)
            } else {
                exit 1                # Exit with error if either value is missing
            }
        }'
}


# Prompt the user to enter one or more database names separated by spaces
read -p "Enter database name(s) (space-separated): " DBS

# Prompt the user to choose the operation mode: full backup, incremental backup, or restore
read -p "Select operation: full | incremental | restore: " MODE


# Create the backup directory if it doesn't exist
mkdir -p "$BACKUP_DIR"

# Record the start time of the operation (in seconds since epoch)
START_TIME=$(date +%s)

# If the selected operation is 'full' backup
if [[ "$MODE" == "full" ]]; then
    # Loop over each specified database
    for DB in $DBS; do
        # Generate a timestamp to uniquely identify this backup
        TIMESTAMP=$(date +"%Y%m%d%H%M%S")

        # Define paths for the SQL dump, metadata file, and tracking file
        BACKUP_FILE="$BACKUP_DIR/${DB}_full_${TIMESTAMP}.sql"
        META_FILE="$BACKUP_DIR/${DB}_meta_${TIMESTAMP}.txt"
        TRACK_FILE="$BACKUP_DIR/${DB}_incremental_start.txt"

        # Log the start of full backup
        log_message "Starting full backup for $DB..."

        # Perform full logical backup using mysqlpump
        mysqlpump -u "$MYSQL_USER" --databases "$DB" > "$BACKUP_FILE"
        
        # Check if backup command was successful
        if [[ $? -ne 0 ]]; then
            log_message "Full backup for $DB failed!"
            continue
        fi

        # Get the size of the backup file
        FILESIZE=$(du -sh "$BACKUP_FILE" | awk '{print $1}')

        # Fetch the current binary log file and position
        read BINLOG_FILE BINLOG_POS <<< "$(get_master_status)"

        # Save binlog info to a metadata file
        echo "Binlog File: $BINLOG_FILE" > "$META_FILE"
        echo "Position: $BINLOG_POS" >> "$META_FILE"

        # Save the binlog start position to be used for next incremental backup
        echo "$BINLOG_FILE $BINLOG_POS" > "$TRACK_FILE"
        log_message "Incremental tracking reset for $DB: $BINLOG_FILE:$BINLOG_POS"

        # Compress the SQL backup and delete the uncompressed version
        gzip "$BACKUP_FILE"

        # Log completion with details
        log_message "Full backup for $DB completed. File: $(basename "${BACKUP_FILE}.gz"), Size: $FILESIZE"
    done


# If the selected operation is 'incremental' backup
elif [[ "$MODE" == "incremental" ]]; then
    # Loop over each specified database
    for DB in $DBS; do
        # Path to the file that tracks the last full backup binlog position
        TRACK_FILE="$BACKUP_DIR/${DB}_incremental_start.txt"

        # Check if the tracking file exists (required for incremental)
        if [[ ! -f "$TRACK_FILE" ]]; then
            log_message "Error: No incremental start info found for $DB. Run full backup first."
            continue
        fi

        # Read the tracked binlog file and start position from tracking file
        read BINLOG_FILE START_POSITION < "$TRACK_FILE"
        log_message "Using tracked start position for $DB: $BINLOG_FILE:$START_POSITION"

        # Generate timestamp to name the new incremental backup
        TIMESTAMP=$(date +"%Y%m%d%H%M%S")
        INC_FILE_RAW="$BACKUP_DIR/${DB}_inc_${TIMESTAMP}.sql"

        log_message "Starting cumulative incremental backup for $DB..."

        # Extract binlog entries for the database starting from last recorded position
        mysqlbinlog --start-position="$START_POSITION" --database="$DB" "/var/lib/mysql/$BINLOG_FILE" > "$INC_FILE_RAW"

        # Check if mysqlbinlog command succeeded
        if [[ $? -ne 0 ]]; then
            log_message "Incremental backup for $DB failed!"
            rm -f "$INC_FILE_RAW"  # Cleanup partial file if failed
            continue
        fi

        # Compress the raw incremental SQL file
        gzip "$INC_FILE_RAW"

        # Get compressed file size
        FILESIZE=$(du -sh "${INC_FILE_RAW}.gz" | awk '{print $1}')

        # Log successful incremental backup
        log_message "Cumulative incremental for $DB completed. File: $(basename "${INC_FILE_RAW}.gz"), Size: $FILESIZE"
    done

# If the selected operation is 'restore'
elif [[ "$MODE" == "restore" ]]; then
    # Loop through each specified database
    for DB in $DBS; do
        log_message "Starting restore for $DB..."

        # Find the most recent full backup for the database (by timestamp in filename)
        FULL_ZIP=$(ls -t "$BACKUP_DIR/${DB}_full_"*.gz 2>/dev/null | head -n1)
        if [[ -z "$FULL_ZIP" ]]; then
            log_message "No full backup found for $DB"
            continue
        fi

        # Drop the database if it exists, then restore it from the full backup
        mysql -u "$MYSQL_USER" -e "DROP DATABASE IF EXISTS $DB;"
        gunzip -c "$FULL_ZIP" | mysql -u "$MYSQL_USER"
        log_message "Full backup restored from $(basename "$FULL_ZIP")"

        # Extract timestamp from full backup filename to locate matching incrementals
        FULL_TS=$(echo "$FULL_ZIP" | grep -oP '\d{14}')

        # Find the latest incremental backup (by filename pattern) — assumes manual control over what to apply
        INC_ZIP=$(ls -t "$BACKUP_DIR/${DB}_inc_"*.gz 2>/dev/null | grep -o "$BACKUP_DIR/${DB}_inc_.*\.gz" | sort | tail -n1)

        # If an incremental backup exists, apply it after the full restore
        if [[ -n "$INC_ZIP" ]]; then
            gunzip -c "$INC_ZIP" | mysql -u "$MYSQL_USER"
            log_message "Incremental backup applied from $(basename "$INC_ZIP")"
        else
            log_message "No incremental backup found after full for $DB."
        fi

        # Log completion of full + (optional) incremental restore
        log_message "Restore completed for $DB"
    done

else
    # If the user input for operation mode is invalid, log and exit
    log_message "Invalid operation. Exiting."
    exit 1
fi

# Capture the end time after the operation
END_TIME=$(date +%s)

# Calculate total elapsed time in seconds
TOTAL_TIME=$((END_TIME - START_TIME))

# Log the total time taken for the selected operation
log_message "Operation '$MODE' finished. Total time: ${TOTAL_TIME} seconds."

