#!/bin/bash

# This script creates a MySQL test database, populates it with data in stages,
# performs a full backup and two incremental backups using Percona XtraBackup,
# prepares the backups, restores the data, and validates the restoration.
# It is designed to be portable with error handling and configuration options.

# Exit immediately if any command fails to ensure no partial execution.
set -e

# Configuration variables (modify these as needed for your environment)
MYSQL_USER="root"                        # MySQL user with sufficient privileges
MYSQL_PASSWORD=""                        # MySQL password (leave empty if none or use .my.cnf)
BACKUP_DIR="/tmp/xtrabackup"            # Directory for full backup
INCREMENTAL_DIR1="/tmp/xtrabackup_inc1" # Directory for first incremental backup
INCREMENTAL_DIR2="/tmp/xtrabackup_inc2" # Directory for second incremental backup
MYSQL_DATA_DIR="/var/lib/mysql"         # MySQL data directory (check your system)
BACKUP_DATA_DIR="${MYSQL_DATA_DIR}.bak" # Backup of original MySQL data directory

# Function to log messages with timestamp
log() {
    echo "[$(date '+%Y-%m-%d %H:%M:%S')] $1"
}

# Function to check for required commands
check_requirements() {
    local required_commands=("mysql" "mysqld" "xtrabackup" "systemctl")
    for cmd in "${required_commands[@]}"; do
        if ! command -v "$cmd" &>/dev/null; then
            log "ERROR: $cmd is not installed or not in PATH."
            log "Please install MySQL and Percona XtraBackup, and ensure systemctl is available."
            exit 1
        fi
    done
}

# Function to check if a directory is writable
check_directory_writable() {
    local dir="$1"
    if [[ ! -d "$dir" ]]; then
        mkdir -p "$dir" || {
            log "ERROR: Cannot create directory $dir. Check permissions."
            exit 1
        }
    fi
    if [[ ! -w "$dir" ]]; then
        log "ERROR: Directory $dir is not writable. Check permissions."
        exit 1
    fi
}

# Function to check MySQL connection
check_mysql_connection() {
    local mysql_cmd="mysql -u${MYSQL_USER}"
    [[ -n "$MYSQL_PASSWORD" ]] && mysql_cmd+=" -p${MYSQL_PASSWORD}"
    if ! $mysql_cmd -e "SELECT 1" &>/dev/null; then
        log "ERROR: Cannot connect to MySQL with provided credentials."
        log "Check MYSQL_USER, MYSQL_PASSWORD, or MySQL configuration."
        exit 1
    fi
}

# Main script starts here
log "Starting MySQL backup and restore test script..."

# Check requirements and environment
check_requirements
check_directory_writable "$(dirname "$BACKUP_DIR")"
check_directory_writable "$(dirname "$INCREMENTAL_DIR1")"
check_directory_writable "$(dirname "$INCREMENTAL_DIR2")"
check_mysql_connection

# MySQL command with optional password
MYSQL="mysql -u${MYSQL_USER}"
[[ -n "$MYSQL_PASSWORD" ]] && MYSQL+=" -p${MYSQL_PASSWORD}"

# Create a 'test' database and tables
log "Creating 'test' database and inserting 100 rows..."
$MYSQL -e "
DROP DATABASE IF EXISTS test;
CREATE DATABASE test;
USE test;
CREATE TABLE orders1 (id INT PRIMARY KEY, item VARCHAR(50));
CREATE TABLE orders2 (id INT PRIMARY KEY, item VARCHAR(50));
"

# Insert 100 rows into both tables
for i in {1..100}; do
    $MYSQL -e "
    USE test;
    INSERT INTO orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO orders2 VALUES ($i, 'Item2_$i');
    "
done

# Perform a full backup
log "Performing full backup to ${BACKUP_DIR}..."
rm -rf "$BACKUP_DIR" # Clear any existing backup
xtrabackup --backup --target-dir="$BACKUP_DIR" --user="$MYSQL_USER" ${MYSQL_PASSWORD:+--password="$MYSQL_PASSWORD"}

# Insert 50 more rows (101-150)
log "Inserting 50 more rows (101-150) into both tables..."
for i in {101..150}; do
    $MYSQL -e "
    USE test;
    INSERT INTO orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO orders2 VALUES ($i, 'Item2_$i');
    "
done

# Perform the first incremental backup
log "Performing first incremental backup to ${INCREMENTAL_DIR1}..."
rm -rf "$INCREMENTAL_DIR1" # Clear any existing incremental backup
xtrabackup --backup --target-dir="$INCREMENTAL_DIR1" --incremental-basedir="$BACKUP_DIR" --user="$MYSQL_USER" ${MYSQL_PASSWORD:+--password="$MYSQL_PASSWORD"}

# Insert 50 more rows (151-200)
log "Inserting 50 more rows (151-200) into both tables..."
for i in {151..200}; do
    $MYSQL -e "
    USE test;
    INSERT INTO orders1 VALUES ($i, 'Item1_$i');
    INSERT INTO orders2 VALUES ($i, 'Item2_$i');
    "
done

# Perform the second incremental backup
log "Performing second incremental backup to ${INCREMENTAL_DIR2}..."
rm -rf "$INCREMENTAL_DIR2" # Clear any existing incremental backup
xtrabackup --backup --target-dir="$INCREMENTAL_DIR2" --incremental-basedir="$INCREMENTAL_DIR1" --user="$MYSQL_USER" ${MYSQL_PASSWORD:+--password="$MYSQL_PASSWORD"}

# Prepare the full backup
log "Preparing full backup (apply-log-only)..."
xtrabackup --prepare --apply-log-only --target-dir="$BACKUP_DIR"

# Apply the first incremental backup
log "Applying first incremental backup..."
xtrabackup --prepare --apply-log-only --target-dir="$BACKUP_DIR" --incremental-dir="$INCREMENTAL_DIR1"

# Apply the second incremental backup and finalize
log "Applying second incremental backup and finalizing..."
xtrabackup --prepare --target-dir="$BACKUP_DIR" --incremental-dir="$INCREMENTAL_DIR2"

# Stop MySQL service
log "Stopping MySQL for restore..."
sudo systemctl stop mysql || {
    log "ERROR: Failed to stop MySQL. Check if systemctl is available or use alternative method."
    exit 1
}

# Back up current MySQL data directory
log "Backing up current data directory to ${BACKUP_DATA_DIR}..."
if [[ -d "$MYSQL_DATA_DIR" ]]; then
    sudo mv "$MYSQL_DATA_DIR" "$BACKUP_DATA_DIR" || {
        log "ERROR: Failed to back up MySQL data directory."
        exit 1
    }
else
    log "Warning: MySQL data directory ${MYSQL_DATA_DIR} does not exist."
fi

# Restore the backup
log "Copying restored data to ${MYSQL_DATA_DIR}..."
xtrabackup --copy-back --target-dir="$BACKUP_DIR"

# Set correct ownership
log "Setting correct permissions on ${MYSQL_DATA_DIR}..."
sudo chown -R mysql:mysql "$MYSQL_DATA_DIR" || {
    log "ERROR: Failed to set permissions. Check if mysql user/group exists."
    exit 1
}

# Start MySQL service
log "Starting MySQL..."
sudo systemctl start mysql || {
    log "ERROR: Failed to start MySQL. Check MySQL configuration or logs."
    exit 1
}

# Validate restored data
log "Validating restored data..."
$MYSQL -e "
USE test;
SELECT COUNT(*) AS orders1_count FROM orders1;
SELECT COUNT(*) AS orders2_count FROM orders2;
"

log "Script completed successfully!"