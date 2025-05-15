#!/bin/bash

# This script creates a MySQL test database, populates it with data in multiple stages,
# performs a full backup and three incremental backups using xtrabackup,
# prepares the backups, restores the data, and validates the restoration.
# It assumes MySQL and xtrabackup are installed and configured.

# Exit immediately if any command fails to ensure no partial execution.
set -e

# Create a 'test' database, drop it if it exists, and create two tables: orders1 and orders2.
# Each table has an 'id' primary key and an 'item' column for storing item names.
echo "Creating 'test' database and inserting 100 rows..."
mysql -u root -e "
-- Remove the 'test' database if it already exists to start fresh.
DROP DATABASE IF EXISTS test;
-- Create a new 'test' database.
CREATE DATABASE test;
-- Switch to the 'test' database for subsequent commands.
USE test;
-- Create 'orders1' table with 'id' as primary key and 'item' as a VARCHAR field.
CREATE TABLE orders1 (id INT PRIMARY KEY, item VARCHAR(50));
-- Create 'orders2' table with the same structure as 'orders1'.
CREATE TABLE orders2 (id INT PRIMARY KEY, item VARCHAR(50));
"

# Insert 100 rows into both 'orders1' and 'orders2' tables.
# Each row has a unique ID (1 to 100) and an item name like 'Item1_1' or 'Item2_1'.
for i in {1..100}; do
  mysql -u root -e "
  -- Switch to the 'test' database.
  USE test;
  -- Insert a row into 'orders1' with the current loop index and formatted item name.
  INSERT INTO orders1 VALUES ($i, 'Item1_$i');
  -- Insert a row into 'orders2' with the same index and a different item name.
  INSERT INTO orders2 VALUES ($i, 'Item2_$i');
  "
done

# Perform a full backup of the MySQL database using xtrabackup.
# The backup is stored in /home/ubuntu/xtrabackup, and the root user is used for access.
echo "Performing full backup..."
xtrabackup --backup --target-dir=/home/ubuntu/xtrabackup --user=root

# Insert an additional 50 rows into both tables (IDs 101 to 150).
# This simulates database changes after the full backup.
echo "Inserting 50 more rows (101-150) into both tables..."
for i in {101..150}; do
  mysql -u root -e "
  -- Switch to the 'test' database.
  USE test;
  -- Insert a row into 'orders1' with the current loop index and formatted item name.
  INSERT INTO orders1 VALUES ($i, 'Item1_$i');
  -- Insert a row into 'orders2' with the same index and a different item name.
  INSERT INTO orders2 VALUES ($i, 'Item2_$i');
  "
done

# Perform the first incremental backup to capture changes since the full backup.
# The backup is stored in /home/ubuntu/xtrabackup_inc1 and references the full backup.
echo "Performing first incremental backup..."
xtrabackup --backup --target-dir=/home/ubuntu/xtrabackup_inc1 --incremental-basedir=/home/ubuntu/xtrabackup --user=root

# Insert another 50 rows into both tables (IDs 151 to 200).
# This simulates further database changes after the first incremental backup.
echo "Inserting 50 more rows (151-200) into both tables..."
for i in {151..200}; do
  THINK
  mysql -u root -e "
  -- Switch to the 'test' database.
  USE test;
  -- Insert a row into 'orders1' with the current loop index and formatted item name.
  INSERT INTO orders1 VALUES ($i, 'Item1_$i');
  -- Insert a row into 'orders2' with the same index and a different item name.
  INSERT INTO orders2 VALUES ($i, 'Item2_$i');
  "
done

# Perform the second incremental backup to capture changes since the first incremental backup.
# The backup is stored in /home/ubuntu/xtrabackup_inc2 and references the first incremental backup.
echo "Performing second incremental backup..."
xtrabackup --backup --target-dir=/home/ubuntu/xtrabackup_inc2 --incremental-basedir=/home/ubuntu/xtrabackup_inc1 --user=root

# Insert another 50 rows into both tables (IDs 201 to 250).
# This simulates further database changes after the second incremental backup.
echo "Inserting 50 more rows (201-250) into both tables..."
for i in {201..250}; do
  mysql -u root -e "
  -- Switch to the 'test' database.
  USE test;
  -- Insert a row into 'orders1' with the current loop index and formatted item name.
  INSERT INTO orders1 VALUES ($i, 'Item1_$i');
  -- Insert a row into 'orders2' with the same index and a different item name.
  INSERT INTO orders2 VALUES ($i, 'Item2_$i');
  "
done

# Perform the third incremental backup to capture changes since the second incremental backup.
# The backup is stored in /home/ubuntu/xtrabackup_inc3 and references the second incremental backup.
echo "Performing third incremental backup..."
xtrabackup --backup --target-dir=/home/ubuntu/xtrabackup_inc3 --incremental-basedir=/home/ubuntu/xtrabackup_inc2 --user=root

# Prepare the full backup by applying the transaction log in read-only mode.
# This makes the backup consistent without finalizing it for restoration.
echo "Preparing full backup (apply-log-only)..."
xtrabackup --prepare --apply-log-only --target-dir=/home/ubuntu/xtrabackup

# Apply the first incremental backup to the full backup.
# This merges changes from the first incremental backup in read-only mode.
echo "Applying first incremental backup..."
xtrabackup --prepare --apply-log-only --target-dir=/home/ubuntu/xtrabackup --incremental-dir=/home/ubuntu/xtrabackup_inc1

# Apply the second incremental backup to the full backup.
# This merges changes from the second incremental backup in read-only mode.
echo "Applying second incremental backup..."
xtrabackup --prepare --apply-log-only --target-dir=/home/ubuntu/xtrabackup --incremental-dir=/home/ubuntu/xtrabackup_inc2

# Apply the third incremental backup and finalize the backup.
# This merges the final changes and completes the recovery process for restoration.
echo "Applying third incremental backup and finalizing..."
xtrabackup --prepare --target-dir=/home/ubuntu/xtrabackup --incremental-dir=/home/ubuntu/xtrabackup_inc3

# Begin the restore process by stopping the MySQL service.
# This ensures no database operations interfere with the restoration.
echo "Stopping MySQL for restore..."
sudo systemctl stop mysql

# Back up the current MySQL data directory to preserve it.
# The existing data is moved to /var/lib/mysql.bak as a precaution.
echo "Backing up current data directory..."
sudo mv /var/lib/mysql /var/lib/mysql.bak

# Copy the prepared backup data to the MySQL data directory.
# This restores the database to the state captured in the backups.
echo "Copying restored data..."
xtrabackup --copy-back --target-dir=/home/ubuntu/xtrabackup

# Set correct ownership for the restored data directory.
# The mysql user and group must own the directory for MySQL to function.
echo "Setting correct permissions..."
sudo chown -R mysql:mysql /var/lib/mysql

# Start the MySQL service to make the restored database accessible.
echo "Starting MySQL..."
sudo systemctl start mysql

# Validate the restored data by checking the row counts in both tables.
# This confirms that the restoration included all 250 rows in each table.
echo "Validating restored data..."
mysql -u root -e "
-- Switch to the 'test' database.
USE test;
-- Count the number of rows in 'orders1' and display as 'orders1_count'.
SELECT COUNT(*) AS orders1_count FROM orders1;
-- Count the number of rows in 'orders2' and display as 'orders2_count'.
SELECT COUNT(*) AS orders2_count FROM orders2;
"