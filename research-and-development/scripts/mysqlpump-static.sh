#!/bin/bash

# Define paths and log file
LOG_FILE="/var/log/mysql_backup_restore.log"
BACKUP_DIR="/home/ubuntu"
MYSQL_USER="root"

# Define color codes for terminal output
COLOR_RESET='\033[0m'
COLOR_GREEN='\033[32m'
COLOR_BLUE='\033[34m'
COLOR_YELLOW='\033[33m'
COLOR_RED='\033[31m'
COLOR_CYAN='\033[36m'

# Function to log messages with timestamp and color output
log_message() {
    TIMESTAMP=$(date "+%Y-%m-%d %H:%M:%S")
    MESSAGE="$1"

    # Log to file
    echo "$TIMESTAMP - $MESSAGE" >> $LOG_FILE

    # Colored output to terminal
    if [[ "$MESSAGE" == *"error"* || "$MESSAGE" == *"fail"* ]]; then
        echo -e "${COLOR_RED}$TIMESTAMP - $MESSAGE${COLOR_RESET}"
    elif [[ "$MESSAGE" == *"completed"* || "$MESSAGE" == *"success"* ]]; then
        echo -e "${COLOR_GREEN}$TIMESTAMP - $MESSAGE${COLOR_RESET}"
    elif [[ "$MESSAGE" == *"backup"* || "$MESSAGE" == *"insert"* || "$MESSAGE" == *"restore"* ]]; then
        echo -e "${COLOR_BLUE}$TIMESTAMP - $MESSAGE${COLOR_RESET}"
    else
        echo -e "${COLOR_CYAN}$TIMESTAMP - $MESSAGE${COLOR_RESET}"
    fi
}

# Step 1: Create database and tables
log_message "Starting database and table creation..."
mysql -u $MYSQL_USER -e "
CREATE DATABASE IF NOT EXISTS test;
USE test;
CREATE TABLE IF NOT EXISTS orders1 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders2 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders3 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders4 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders5 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders6 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders7 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders8 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders9 (id INT, item VARCHAR(50));
CREATE TABLE IF NOT EXISTS orders10 (id INT, item VARCHAR(50));
"
log_message "Database and tables created."

# Step 2: Insert data into tables
log_message "Inserting data into tables..."
for i in {1..1000}; do
  mysql -u $MYSQL_USER -e "
    INSERT INTO test.orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO test.orders2 VALUES ($i, 'Item2_$i');
    INSERT INTO test.orders3 VALUES ($i, 'Item3_$i');
    INSERT INTO test.orders4 VALUES ($i, 'Item4_$i');
    INSERT INTO test.orders5 VALUES ($i, 'Item5_$i');
    INSERT INTO test.orders6 VALUES ($i, 'Item6_$i');
    INSERT INTO test.orders7 VALUES ($i, 'Item7_$i');
    INSERT INTO test.orders8 VALUES ($i, 'Item8_$i');
    INSERT INTO test.orders9 VALUES ($i, 'Item9_$i');
    INSERT INTO test.orders10 VALUES ($i, 'Item10_$i');
  "
done
log_message "Data insertion completed."

# Step 3: Full Backup using mysqlpump
log_message "Starting full backup..."
mysqlpump -u $MYSQL_USER --add-drop-database --add-drop-table \
--exclude-databases=mysql,performance_schema,sys,information_schema \
--databases test --result-file=$BACKUP_DIR/full_backup.sql
log_message "Full backup completed."

# Step 4: Record the binlog position for the first incremental backup
log_message "Recording binlog position for incremental backup..."
START_POSITION=$(mysql -u $MYSQL_USER -e "SHOW MASTER STATUS\G" | grep 'Position' | awk '{print $2}')
log_message "Binlog position recorded: $START_POSITION"

# Step 5: Insert more data (151-200)
log_message "Inserting more data (151-200)..."
for i in {1001..2000}; do
  mysql -u $MYSQL_USER -e "
    INSERT INTO test.orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO test.orders2 VALUES ($i, 'Item2_$i');
    INSERT INTO test.orders3 VALUES ($i, 'Item3_$i');
    INSERT INTO test.orders4 VALUES ($i, 'Item4_$i');
    INSERT INTO test.orders5 VALUES ($i, 'Item5_$i');
    INSERT INTO test.orders6 VALUES ($i, 'Item6_$i');
    INSERT INTO test.orders7 VALUES ($i, 'Item7_$i');
    INSERT INTO test.orders8 VALUES ($i, 'Item8_$i');
    INSERT INTO test.orders9 VALUES ($i, 'Item9_$i');
    INSERT INTO test.orders10 VALUES ($i, 'Item10_$i');
  "
done
log_message "Data insertion (151-200) completed."

# Step 6: Perform incremental backup (inc1)
log_message "Extracting incremental backup (inc1) starting from position $START_POSITION..."
mysqlbinlog --start-position=$START_POSITION /var/lib/mysql/mysql-bin.000001 > $BACKUP_DIR/inc1.sql
log_message "Incremental backup (inc1) completed."

# Step 7: Record the new binlog position for the next incremental backup
log_message "Recording binlog position for second incremental backup..."
NEW_START_POSITION=$(mysql -u $MYSQL_USER -e "SHOW MASTER STATUS\G" | grep 'Position' | awk '{print $2}')
log_message "New binlog position recorded: $NEW_START_POSITION"

# Step 8: Insert more data (201-300)
log_message "Inserting more data (201-300)..."
for i in {2001..3000}; do
  mysql -u $MYSQL_USER -e "
    INSERT INTO test.orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO test.orders2 VALUES ($i, 'Item2_$i');
    INSERT INTO test.orders3 VALUES ($i, 'Item3_$i');
    INSERT INTO test.orders4 VALUES ($i, 'Item4_$i');
    INSERT INTO test.orders5 VALUES ($i, 'Item5_$i');
    INSERT INTO test.orders6 VALUES ($i, 'Item6_$i');
    INSERT INTO test.orders7 VALUES ($i, 'Item7_$i');
    INSERT INTO test.orders8 VALUES ($i, 'Item8_$i');
    INSERT INTO test.orders9 VALUES ($i, 'Item9_$i');
    INSERT INTO test.orders10 VALUES ($i, 'Item10_$i');
  "
done
log_message "Data insertion (201-300) completed."

# Step 9: Perform second incremental backup (inc2)
log_message "Extracting incremental backup (inc2) starting from position $NEW_START_POSITION..."
mysqlbinlog --start-position=$NEW_START_POSITION /var/lib/mysql/mysql-bin.000001 > $BACKUP_DIR/inc2.sql
log_message "Incremental backup (inc2) completed."

# Step 10: Restore the database from full backup and incremental backups
log_message "Starting database restore..."
mysql -u $MYSQL_USER -e "DROP DATABASE IF EXISTS test;"
mysql -u $MYSQL_USER < $BACKUP_DIR/full_backup.sql
mysql -u $MYSQL_USER < $BACKUP_DIR/inc1.sql
mysql -u $MYSQL_USER < $BACKUP_DIR/inc2.sql
log_message "Database restore completed."

# Step 11: Verify restore
log_message "Verifying the restore..."
mysql -u $MYSQL_USER -e "
USE test;
SELECT 'orders1' AS table_name, COUNT(*) FROM orders1
UNION
SELECT 'orders2', COUNT(*) FROM orders2
UNION
SELECT 'orders3', COUNT(*) FROM orders3
UNION
SELECT 'orders4', COUNT(*) FROM orders4
UNION
SELECT 'orders5', COUNT(*) FROM orders5
UNION
SELECT 'orders6', COUNT(*) FROM orders6
UNION
SELECT 'orders7', COUNT(*) FROM orders7
UNION
SELECT 'orders8', COUNT(*) FROM orders8
UNION
SELECT 'orders9', COUNT(*) FROM orders9
UNION
SELECT 'orders10', COUNT(*) FROM orders10;
"
log_message "Restore verification completed."

log_message "Script execution completed."
