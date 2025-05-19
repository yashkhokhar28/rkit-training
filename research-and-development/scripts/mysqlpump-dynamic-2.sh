#!/bin/bash

LOG_FILE="/var/log/mysql_backup_restore.log" # location for logging events.
BACKUP_DIR="/home/ubuntu/backups" # where backups will be stored.
MYSQL_USER="root" # MySQL user used for all operations.

# ANSI escape codes used to color the console output.
COLOR_RESET='\033[0m'
COLOR_GREEN='\033[32m'
COLOR_BLUE='\033[34m'
COLOR_RED='\033[31m'
COLOR_CYAN='\033[36m'

# Logging Function

# Prefixes all logs with a timestamp and writes to log file.
log_message() {
    TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
    echo "$TIMESTAMP - $1" >> "$LOG_FILE"
    if [[ "$1" == *"error"* || "$1" == *"fail"* ]]; then
        echo -e "${COLOR_RED}$TIMESTAMP - $1${COLOR_RESET}" # Red color for errors/failures.
    elif [[ "$1" == *"completed"* || "$1" == *"success"* ]]; then
        echo -e "${COLOR_GREEN}$TIMESTAMP - $1${COLOR_RESET}" # Green for success messages.
    elif [[ "$1" == *"backup"* || "$1" == *"restore"* ]]; then
        echo -e "${COLOR_BLUE}$TIMESTAMP - $1${COLOR_RESET}" # Blue for backup/restore operations.
    else
        echo -e "${COLOR_CYAN}$TIMESTAMP - $1${COLOR_RESET}" # Cyan for other general messages.
    fi
}

# Get Current Binlog Position

# Queries the current binlog file and position from MySQL master status.
# Required for both full and incremental backups to ensure changes are tracked accurately.

get_master_status() {
    mysql -u "$MYSQL_USER" -e "SHOW MASTER STATUS\G" | awk '
        /File:/ { file=$2 }
        /Position:/ { pos=$2 }
        END {
            if (file && pos) { print file, pos } else { exit 1 }
        }'
}

# Prompt User for Input
read -p "Enter database name(s) (space-separated): " DBS  # Prompts for databases to back up or restore.
read -p "Select operation: full | incremental | restore: " MODE # Prompts for operation mode.
mkdir -p "$BACKUP_DIR" # Creates backup directory if not existing.
START_TIME=$(date +%s) # Stores script start time to calculate duration.


# Full Backup Section
if [[ "$MODE" == "full" ]]; then  # Checks if selected mode is full backup.
    for DB in $DBS; do # Loops over all databases entered.

        # Generates timestamped filenames for backup, metadata, and tracking incremental start.
        TIMESTAMP=$(date +"%Y%m%d%H%M%S")
        BACKUP_FILE="$BACKUP_DIR/${DB}_full_${TIMESTAMP}.sql"
        META_FILE="$BACKUP_DIR/${DB}_meta_${TIMESTAMP}.txt"
        TRACK_FILE="$BACKUP_DIR/${DB}_incremental_start.txt"

        # Gets current binlog file and position (start point for next incremental).
        read BINLOG_FILE BINLOG_POS <<< "$(get_master_status)"

        # Saves start point to a tracking file and metadata file.
        echo "$BINLOG_FILE $BINLOG_POS" > "$TRACK_FILE"
        echo "Binlog File: $BINLOG_FILE" > "$META_FILE"
        echo "Position: $BINLOG_POS" >> "$META_FILE"
        log_message "Recorded binlog start for $DB: $BINLOG_FILE:$BINLOG_POS"

        # Runs mysqlpump for a consistent logical backup with --single-transaction for InnoDB tables.
        log_message "Starting full backup for $DB..."
        mysqlpump -u "$MYSQL_USER" --single-transaction --databases "$DB" > "$BACKUP_FILE"

        # Skips to next DB if the backup fails.
        if [[ $? -ne 0 ]]; then
            log_message "Full backup for $DB failed!"
            continue
        fi

        # Logs file size and compresses the SQL file.
        FILESIZE=$(du -sh "$BACKUP_FILE" | awk '{print $1}')
        gzip "$BACKUP_FILE"
        log_message "Full backup for $DB completed. File: $(basename "${BACKUP_FILE}.gz"), Size: $FILESIZE"
    done


# Incremental (Cumulative) Backup
elif [[ "$MODE" == "incremental" ]]; then
    for DB in $DBS; do # For each DB in input.

        # Requires the tracking file from a previous full backup.
        TRACK_FILE="$BACKUP_DIR/${DB}_incremental_start.txt"
        if [[ ! -f "$TRACK_FILE" ]]; then
            log_message "Error: No incremental start info found for $DB. Run full backup first."
            continue
        fi

        # Loads previous position and updates with the current binlog end position.
        read BINLOG_FILE START_POSITION < "$TRACK_FILE"
        read CUR_BINLOG CUR_POS <<< "$(get_master_status)"
        echo "$CUR_BINLOG $CUR_POS" > "$TRACK_FILE"
        log_message "Tracking new end position for $DB: $CUR_BINLOG:$CUR_POS"

        # Determines all binlog files between START_POSITION and CUR_POS.
        BINLOG_LIST=($(mysql -u "$MYSQL_USER" -e "SHOW BINARY LOGS;" | awk 'NR>1 {print $1}'))
        FILES_TO_USE=()
        FOUND=0
        for file in "${BINLOG_LIST[@]}"; do
            if [[ "$file" == "$BINLOG_FILE" ]]; then FOUND=1; fi
            [[ $FOUND -eq 1 ]] && FILES_TO_USE+=("$file")
            [[ "$file" == "$CUR_BINLOG" ]] && break
        done

        # Creates an output filename for incremental.
        TIMESTAMP=$(date +"%Y%m%d%H%M%S")
        INC_FILE_RAW="$BACKUP_DIR/${DB}_inc_${TIMESTAMP}.sql"

        # Extracts changes for the specific DB between the two binlog positions.
        log_message "Creating cumulative incremental backup for $DB..."
        mysqlbinlog --start-position="$START_POSITION" \
                    --stop-position="$CUR_POS" \
                    --database="$DB" \
                    ${FILES_TO_USE[@]/#//var/lib/mysql/} > "$INC_FILE_RAW"

        # Removes partial file if failed.
        if [[ $? -ne 0 ]]; then
            log_message "Incremental backup for $DB failed!"
            rm -f "$INC_FILE_RAW"
            continue
        fi

        # Compresses and logs incremental backup.
        gzip "$INC_FILE_RAW"
        FILESIZE=$(du -sh "${INC_FILE_RAW}.gz" | awk '{print $1}')
        log_message "Cumulative incremental for $DB completed. File: $(basename "${INC_FILE_RAW}.gz"), Size: $FILESIZE"
    done

# Restore Section
elif [[ "$MODE" == "restore" ]]; then  # Begins the restore process.
    for DB in $DBS; do
        log_message "Starting restore for $DB..."

        # Finds the most recent full backup.
        FULL_ZIP=$(ls -t "$BACKUP_DIR/${DB}_full_"*.gz 2>/dev/null | head -n1)
        if [[ -z "$FULL_ZIP" ]]; then
            log_message "No full backup found for $DB"
            continue
        fi

        # Drops DB, restores full backup from gzip archive.
        mysql -u "$MYSQL_USER" -e "DROP DATABASE IF EXISTS $DB;"
        gunzip -c "$FULL_ZIP" | mysql -u "$MYSQL_USER"
        log_message "Full backup restored from $(basename "$FULL_ZIP")"

        # If incremental exists, applies it.
        INC_ZIP=$(ls -t "$BACKUP_DIR/${DB}_inc_"*.gz 2>/dev/null | head -n1)
        if [[ -n "$INC_ZIP" ]]; then
            gunzip -c "$INC_ZIP" | mysql -u "$MYSQL_USER"
            log_message "Incremental backup applied from $(basename "$INC_ZIP")"
        else
            log_message "No incremental backup found after full for $DB."
        fi

        log_message "Restore completed for $DB"
    done

# Invalid Input Handling & Timing
# Invalid mode exits gracefully.
else
    log_message "Invalid operation. Exiting."
    exit 1
fi

# Logs total duration of operation.
END_TIME=$(date +%s)
TOTAL_TIME=$((END_TIME - START_TIME))
log_message "Operation '$MODE' finished. Total time: ${TOTAL_TIME} seconds."