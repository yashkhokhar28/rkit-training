#!/bin/bash

# Variables
DB_NAME="test_db_1"
BACKUP_USER="backup_user"
BACKUP_PASS="Miracle@1234"
BACKUP_DIR="/home/ubuntu/mysql_partial_backup"
MYSQL_DATA_DIR="/var/lib/mysql"
TABLE_LIST_FILE="/home/ubuntu/table_list.txt"

echo "Starting MySQL Partial Backup for $DB_NAME..."

# Step 1: Ensure Backup User Exists
mysql -u root -p -e "
CREATE USER IF NOT EXISTS '$BACKUP_USER'@'localhost' IDENTIFIED BY '$BACKUP_PASS';
GRANT RELOAD, LOCK TABLES, SELECT, BACKUP_ADMIN, PROCESS, REPLICATION CLIENT, CREATE TABLESPACE ON *.* TO '$BACKUP_USER'@'localhost';
FLUSH PRIVILEGES;"

echo "Backup user checked."

# Step 2: Ensure Backup Directory Exists
if [ ! -d "$BACKUP_DIR" ]; then
    mkdir -p "$BACKUP_DIR"
    echo "Created backup directory: $BACKUP_DIR"
fi

# Step 3: Generate Fully Qualified Table List for `test_db_1`
mysql -u root -p -Nse "SELECT CONCAT(table_schema, '.', table_name) FROM information_schema.tables WHERE table_schema='$DB_NAME';" > "$TABLE_LIST_FILE"

if [ ! -s "$TABLE_LIST_FILE" ]; then
    echo "Error: No tables found in $DB_NAME!"
    exit 1
fi

echo "Retrieved all table names for $DB_NAME."

# Step 4: Perform Backup with Time & Space Tracking (Silent Mode)
START_BACKUP_TIME=$(date +%s)

xtrabackup --backup \
  --datadir=$MYSQL_DATA_DIR \
  --target-dir=$BACKUP_DIR \
  --tables-file=$TABLE_LIST_FILE \
  --tables-exclude='performance_schema.keyring_component_status' \
  --user=$BACKUP_USER --password=$BACKUP_PASS \
  >/dev/null 2>&1  

END_BACKUP_TIME=$(date +%s)
BACKUP_DURATION=$((END_BACKUP_TIME - START_BACKUP_TIME))

# Step 5: Get Backup Size
BACKUP_SIZE=$(du -sh $BACKUP_DIR | awk '{print $1}')

echo "Backup completed in $BACKUP_DURATION seconds. Size: $BACKUP_SIZE."

# Step 6: Verify Backup Exists Before Proceeding
if [ ! -d "$BACKUP_DIR/$DB_NAME" ]; then
    echo "Error: Backup directory for $DB_NAME not found!"
    exit 1
fi

# Step 7: Prepare the Backup for Restore (Silent Mode)
echo "Preparing backup for restore..."
xtrabackup --prepare --target-dir=$BACKUP_DIR >/dev/null 2>&1
echo "Backup is ready for restoration."

# Step 8: Stop MySQL Temporarily
echo "Stopping MySQL..."
sudo systemctl stop mysql

# Step 9: Restore `test_db_1` Without Affecting Other Databases (Silent Mode)
START_RESTORE_TIME=$(date +%s)

echo "Restoring $DB_NAME..."
sudo cp -R $BACKUP_DIR/$DB_NAME $MYSQL_DATA_DIR/ >/dev/null 2>&1
sudo chown -R mysql:mysql $MYSQL_DATA_DIR/$DB_NAME >/dev/null 2>&1

END_RESTORE_TIME=$(date +%s)
RESTORE_DURATION=$((END_RESTORE_TIME - START_RESTORE_TIME))

# Step 10: Restart MySQL
echo "Restarting MySQL..."
sudo systemctl start mysql

# Step 11: Verify Restoration
echo "Verifying restoration..."
mysql -u root -p -e "SHOW DATABASES;"
mysql -u root -p -e "USE $DB_NAME; SHOW TABLES;"

echo "Backup & Restore for $DB_NAME Completed Successfully!"
echo "Restore completed in $RESTORE_DURATION seconds."