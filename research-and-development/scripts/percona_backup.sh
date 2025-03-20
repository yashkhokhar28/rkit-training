#!/bin/bash

# Backup directory
BASE_DIR="/home/ubuntu/backup"
LOG_FILE="/var/log/percona_backup.log"

# Ensure the log file exists
touch "$LOG_FILE"

# Function to log messages
log_message() {
    echo "$(date '+%Y-%m-%d %H:%M:%S') - $1" | tee -a "$LOG_FILE"
}

# Function to take a full backup
full_backup() {
    mkdir -p "$BASE_DIR/full_backup"
    log_message "Starting Full Backup..."
    START_TIME=$(date +%s)

    mysql -e "FLUSH TABLES WITH READ LOCK;"

    xtrabackup --backup --target-dir="$BASE_DIR/full_backup" --datadir=/var/lib/mysql 2>&1 | tee -a "$LOG_FILE"

    if [ $? -eq 0 ]; then
        log_message "Full Backup Completed Successfully."
    else
        log_message "Full Backup Failed."
    fi

    mysql -e "UNLOCK TABLES;"

    END_TIME=$(date +%s)
    log_message "Full Backup Time: $((END_TIME - START_TIME)) seconds."
}

# Function to restore full backup
restore_full_backup() {
    if [ ! -d "$BASE_DIR/full_backup" ]; then
        log_message "ERROR: Full backup not found."
        return
    fi

    log_message "Restoring Full Backup..."
    START_TIME=$(date +%s)

    xtrabackup --prepare --target-dir="$BASE_DIR/full_backup" 2>&1 | tee -a "$LOG_FILE"
    xtrabackup --copy-back --target-dir="$BASE_DIR/full_backup" 2>&1 | tee -a "$LOG_FILE"

    chown -R mysql:mysql /var/lib/mysql
    mysql -e "FLUSH TABLES WITH READ LOCK;"

    END_TIME=$(date +%s)
    log_message "Full Backup Restoration Completed in $((END_TIME - START_TIME)) seconds."
}

# Function to take an incremental backup
incremental_backup() {
    mkdir -p "$BASE_DIR/incremental_backup"
    if [ ! -d "$BASE_DIR/full_backup" ]; then
        log_message "ERROR: Full backup not found. Please take a full backup first."
        return
    fi

    log_message "Starting Incremental Backup..."
    START_TIME=$(date +%s)

    mysql -e "FLUSH TABLES WITH READ LOCK;"

    xtrabackup --backup --target-dir="$BASE_DIR/incremental_backup" --incremental-basedir="$BASE_DIR/full_backup" --datadir=/var/lib/mysql 2>&1 | tee -a "$LOG_FILE"

    if [ $? -eq 0 ]; then
        log_message "Incremental Backup Completed Successfully."
    else
        log_message "Incremental Backup Failed."
    fi

    mysql -e "UNLOCK TABLES;"

    END_TIME=$(date +%s)
    log_message "Incremental Backup Time: $((END_TIME - START_TIME)) seconds."
}

# Function to restore incremental backup
restore_incremental_backup() {
    if [ ! -d "$BASE_DIR/full_backup" ]; then
        log_message "ERROR: Full backup is required before restoring incremental backups."
        return
    fi

    log_message "Restoring Incremental Backup..."
    START_TIME=$(date +%s)

    xtrabackup --prepare --apply-log-only --target-dir="$BASE_DIR/full_backup" --incremental-dir="$BASE_DIR/incremental_backup" 2>&1 | tee -a "$LOG_FILE"
    xtrabackup --copy-back --target-dir="$BASE_DIR/full_backup" 2>&1 | tee -a "$LOG_FILE"

    chown -R mysql:mysql /var/lib/mysql
    mysql -e "FLUSH TABLES WITH READ LOCK;"

    END_TIME=$(date +%s)
    log_message "Incremental Backup Restoration Completed in $((END_TIME - START_TIME)) seconds."
}

# Function to take a partial backup
partial_backup() {
    mkdir -p "$BASE_DIR/partial_backup"
    read -p "Enter the database name for partial backup: " DB_NAME

    if ! mysql -e "USE $DB_NAME;" 2>/dev/null; then
        log_message "ERROR: Database $DB_NAME does not exist."
        return
    fi

    TABLES_FILE="$BASE_DIR/tables_list.txt"
    echo "" >"$TABLES_FILE" # Clear the tables list

    echo -e "\nDo you want to back up all tables in $DB_NAME? (yes/no)"
    read -r ALL_TABLES

    if [[ "$ALL_TABLES" == "yes" ]]; then
        mysql -N -D "$DB_NAME" -e "SHOW TABLES;" | awk -v db="$DB_NAME" '{print db"."$1}' >"$TABLES_FILE"
    else
        echo "Enter the table names to back up (space-separated):"
        read -r SELECTED_TABLES
        for TABLE in $SELECTED_TABLES; do
            echo "$DB_NAME.$TABLE" >>"$TABLES_FILE"
        done
    fi

    log_message "Starting Partial Backup for $DB_NAME..."
    START_TIME=$(date +%s)

    mysql -e "FLUSH TABLES WITH READ LOCK;"

    xtrabackup --backup --target-dir="$BASE_DIR/partial_backup" --datadir=/var/lib/mysql --tables-file="$TABLES_FILE" 2>&1 | tee -a "$LOG_FILE"

    if [ $? -eq 0 ]; then
        log_message "Partial Backup Completed Successfully for $(cat "$TABLES_FILE")."
    else
        log_message "Partial Backup Failed."
    fi

    mysql -e "UNLOCK TABLES;"

    END_TIME=$(date +%s)
    log_message "Partial Backup Time: $((END_TIME - START_TIME)) seconds."
}

# Function to restore partial backup
restore_partial_backup() {
    if [ ! -d "$BASE_DIR/partial_backup" ]; then
        log_message "ERROR: Partial backup not found."
        return
    fi

    read -p "Enter the database name to restore: " DB_NAME

    log_message "Preparing Partial Backup for Restoration..."
    xtrabackup --prepare --target-dir="$BASE_DIR/partial_backup" 2>&1 | tee -a "$LOG_FILE"

    log_message "Restoring all tables from $DB_NAME..."
    DB_DIR="$BASE_DIR/partial_backup/$DB_NAME"
    if [ -d "$DB_DIR" ]; then
        cp -r "$DB_DIR" "/var/lib/mysql/"
        chown -R mysql:mysql "/var/lib/mysql/$DB_NAME"
    else
        log_message "ERROR: Database directory $DB_DIR does not exist."
    fi

    log_message "Partial Backup Restoration Completed."
}

# Function to take a compressed backup
compressed_backup() {
    mkdir -p "$BASE_DIR/compressed_backup"
    log_message "Starting Compressed Backup..."
    START_TIME=$(date +%s)

    mysql -e "FLUSH TABLES WITH READ LOCK;"

    xtrabackup --backup --compress --target-dir="$BASE_DIR/compressed_backup" --datadir=/var/lib/mysql 2>&1 | tee -a "$LOG_FILE"

    if [ $? -eq 0 ]; then
        log_message "Compressed Backup Completed Successfully."
    else
        log_message "Compressed Backup Failed."
    fi

    mysql -e "UNLOCK TABLES;"

    END_TIME=$(date +%s)
    log_message "Compressed Backup Time: $((END_TIME - START_TIME)) seconds."
}

# Function to restore compressed backup
restore_compressed_backup() {
    if [ ! -d "$BASE_DIR/compressed_backup" ]; then
        log_message "ERROR: Compressed backup not found."
        return
    fi

    log_message "Restoring Compressed Backup..."
    START_TIME=$(date +%s)

    xtrabackup --decompress --target-dir="$BASE_DIR/compressed_backup" 2>&1 | tee -a "$LOG_FILE"
    xtrabackup --prepare --target-dir="$BASE_DIR/compressed_backup" 2>&1 | tee -a "$LOG_FILE"
    xtrabackup --copy-back --target-dir="$BASE_DIR/compressed_backup" 2>&1 | tee -a "$LOG_FILE"

    chown -R mysql:mysql /var/lib/mysql
    mysql -e "FLUSH TABLES WITH READ LOCK;"

    END_TIME=$(date +%s)
    log_message "Compressed Backup Restoration Completed in $((END_TIME - START_TIME)) seconds."
}

# Function to take an encrypted backup
encrypted_backup() {
    mkdir -p "$BASE_DIR/encrypted_backup"
    log_message "Starting Encrypted Backup..."
    START_TIME=$(date +%s)

    mysql -e "FLUSH TABLES WITH READ LOCK;"

    xtrabackup --backup --encrypt=AES256 --encrypt-key="your-encryption-key" --target-dir="$BASE_DIR/encrypted_backup" --datadir=/var/lib/mysql 2>&1 | tee -a "$LOG_FILE"

    if [ $? -eq 0 ]; then
        log_message "Encrypted Backup Completed Successfully."
    else
        log_message "Encrypted Backup Failed."
    fi

    mysql -e "UNLOCK TABLES;"

    END_TIME=$(date +%s)
    log_message "Encrypted Backup Time: $((END_TIME - START_TIME)) seconds."
}

# Function to restore encrypted backup
restore_encrypted_backup() {
    if [ ! -d "$BASE_DIR/encrypted_backup" ]; then
        log_message "ERROR: Encrypted backup not found."
        return
    fi

    log_message "Restoring Encrypted Backup..."
    START_TIME=$(date +%s)

    xtrabackup --decrypt=AES256 --encrypt-key="your-encryption-key" --target-dir="$BASE_DIR/encrypted_backup" 2>&1 | tee -a "$LOG_FILE"
    xtrabackup --prepare --target-dir="$BASE_DIR/encrypted_backup" 2>&1 | tee -a "$LOG_FILE"
    xtrabackup --copy-back --target-dir="$BASE_DIR/encrypted_backup" 2>&1 | tee -a "$LOG_FILE"

    chown -R mysql:mysql /var/lib/mysql
    mysql -e "FLUSH TABLES WITH READ LOCK;"

    END_TIME=$(date +%s)
    log_message "Encrypted Backup Restoration Completed in $((END_TIME - START_TIME)) seconds."
}

# Infinite loop menu
while true; do
    echo -e "\n=== Percona XtraBackup Menu ==="
    echo "1. Full Backup"
    echo "2. Incremental Backup"
    echo "3. Partial Backup"
    echo "4. Compressed Backup"
    echo "5. Encrypted Backup"
    echo "6. Restore Full Backup"
    echo "7. Restore Incremental Backup"
    echo "8. Restore Partial Backup"
    echo "9. Restore Compressed Backup"
    echo "10. Restore Encrypted Backup"
    echo "0. Exit"
    read -p "Select an option: " CHOICE

    case $CHOICE in
    1) full_backup ;;
    2) incremental_backup ;;
    3) partial_backup ;;
    4) compressed_backup ;;
    5) encrypted_backup ;;
    6) restore_full_backup ;;
    7) restore_incremental_backup ;;
    8) restore_partial_backup ;;
    9) restore_compressed_backup ;;
    10) restore_encrypted_backup ;;
    0)
        log_message "Exiting script."
        exit 0
        ;;
    *) echo "Invalid option, please try again." ;;
    esac
done
