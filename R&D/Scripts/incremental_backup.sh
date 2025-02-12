#!/bin/bash

# Variables
DB_NAME="test_db_1"
BACKUP_USER="backup_user"
BACKUP_PASS="Miracle@1234"
BACKUP_DIR="/home/ubuntu/mysql_partial_backup"
INC_BACKUP_DIR="/home/ubuntu/mysql_partial_backup/inc"
MYSQL_DATA_DIR="/var/lib/mysql"
TABLE_LIST_FILE="/home/ubuntu/table_list.txt"

# Function: Ensure Backup User Exists
create_backup_user() {
    mysql -u root -p -e "
    CREATE USER IF NOT EXISTS '$BACKUP_USER'@'localhost' IDENTIFIED BY '$BACKUP_PASS';
    GRANT RELOAD, LOCK TABLES, SELECT, BACKUP_ADMIN, PROCESS, REPLICATION CLIENT, CREATE TABLESPACE ON *.* TO '$BACKUP_USER'@'localhost';
    FLUSH PRIVILEGES;"
    echo "Backup user checked."
}

# Function: Ensure Backup Directories Exist
setup_directories() {
    mkdir -p "$BACKUP_DIR" "$INC_BACKUP_DIR"
    echo "Backup directories checked."
}

# Function: Generate Table List
generate_table_list() {
    mysql -u root -p -Nse "SELECT CONCAT(table_schema, '.', table_name) FROM information_schema.tables WHERE table_schema='$DB_NAME';" > "$TABLE_LIST_FILE"

    if [ ! -s "$TABLE_LIST_FILE" ]; then
        echo " Error: No tables found in $DB_NAME!"
        exit 1
    fi
    echo "Retrieved all table names for $DB_NAME."
}

# Function: Take Partial Full Backup
partial_backup() {
    create_backup_user
    setup_directories
    generate_table_list

    echo "Starting Full Partial Backup..."
    START_FULL_TIME=$(date +%s)

    while IFS= read -r table; do
        echo "Backing up table: $table"
    done < "$TABLE_LIST_FILE" &  # Run in the background to display tables

    xtrabackup --backup \
      --datadir=$MYSQL_DATA_DIR \
      --target-dir=$BACKUP_DIR \
      --tables-file=$TABLE_LIST_FILE \
      --user=$BACKUP_USER --password=$BACKUP_PASS >/dev/null 2>&1  

    END_FULL_TIME=$(date +%s)
    FULL_BACKUP_DURATION=$((END_FULL_TIME - START_FULL_TIME))

    echo "Full partial backup completed in $FULL_BACKUP_DURATION seconds."
}

# Function: Take Incremental Backup
incremental_backup() {
    create_backup_user
    setup_directories

    echo "Taking Incremental Backup..."
    START_INC_TIME=$(date +%s)

    xtrabackup --backup \
      --datadir=$MYSQL_DATA_DIR \
      --target-dir=$INC_BACKUP_DIR \
      --incremental-basedir=$BACKUP_DIR \
      --user=$BACKUP_USER --password=$BACKUP_PASS >/dev/null 2>&1  

    END_INC_TIME=$(date +%s)
    INC_BACKUP_DURATION=$((END_INC_TIME - START_INC_TIME))

    echo "Incremental backup completed in $INC_BACKUP_DURATION seconds."
}

# Function: Prepare and Restore Backup
restore_backup() {
    echo "Preparing full backup..."
    xtrabackup --prepare --apply-log-only --target-dir=$BACKUP_DIR >/dev/null 2>&1
    echo "Full backup prepared!"

    echo "Applying incremental backup..."
    xtrabackup --prepare --apply-log-only --target-dir=$BACKUP_DIR --incremental-dir=$INC_BACKUP_DIR >/dev/null 2>&1
    echo "Incremental backup applied!"

    echo "Stopping MySQL..."
    sudo systemctl stop mysql

    echo "Restoring database..."
    while IFS= read -r table; do
        TABLE_NAME=$(echo "$table" | cut -d '.' -f2)
        echo "Restoring table: $TABLE_NAME"
    done < "$TABLE_LIST_FILE"

    sudo cp -R $BACKUP_DIR/$DB_NAME $MYSQL_DATA_DIR/
    sudo chown -R mysql:mysql $MYSQL_DATA_DIR/$DB_NAME

    echo "Restarting MySQL..."
    sudo systemctl start mysql

    echo "Restoration completed successfully!"
}

# Display Menu to User
echo "Choose an option:"
echo "1 Take Full Partial Backup"
echo "2 Take Incremental Backup"
echo "3 Restore Backup"
read -p "Enter your choice (1/2/3): " choice

case $choice in
    1) partial_backup ;;
    2) incremental_backup ;;
    3) restore_backup ;;
    *) echo "Invalid choice. Exiting." ;;
esac
