#!/bin/bash

# This script resets and configures a MySQL installation on a Linux system.
# It stops the MySQL service, removes and recreates the data directory,
# initializes the database, retrieves the temporary password,
# starts the service, and configures root and remote user accounts.

# Stop the MySQL service to ensure no database operations are in progress.
# This prevents data corruption during the reset process.
sudo systemctl stop mysql

# Remove the existing MySQL data directory to clear all previous data.
# This is a destructive operation and will delete all databases.
sudo rm -rf /var/lib/mysql

# Recreate the MySQL data directory with the correct structure.
# The -p flag ensures parent directories are created if they don't exist.
sudo mkdir -p /var/lib/mysql

# Set ownership of the data directory to the mysql user and group.
# This ensures MySQL has the necessary permissions to access its data.
sudo chown -R mysql:mysql /var/lib/mysql

# Initialize the MySQL data directory with default system databases.
# The --initialize option creates a new data directory securely.
# The --user=mysql ensures the process runs as the mysql user.
# The --datadir specifies the location of the data directory.
sudo mysqld --initialize --user=mysql --datadir=/var/lib/mysql

# Display the temporary root password generated during initialization.
# This password is stored in the MySQL error log and is needed for initial login.
# The grep command filters for the line containing 'temporary password'.
sudo cat /var/log/mysql/error.log | grep 'temporary password'

# Start the MySQL service to allow database connections.
# This makes the MySQL server available for the subsequent configuration steps.
sudo systemctl start mysql

# Execute MySQL commands to configure user accounts.
# The -u root -p flags specify the root user and prompt for the temporary password.
# The heredoc (<<EOF) passes multiple SQL commands to the MySQL client.
mysql -u root -p <<EOF
-- Change the root user's password to 'Miracle@1234'.
-- This updates the root account for localhost to use a new, secure password.
ALTER USER 'root'@'localhost' IDENTIFIED BY 'Miracle@1234';

-- Create a new user 'remote' that can connect from any host ('%').
-- This user is intended for remote access with the password 'remote'.
CREATE USER 'remote'@'%' IDENTIFIED BY 'remote';

-- Grant all privileges to the 'remote' user for all databases and tables.
-- The WITH GRANT OPTION allows the remote user to grant privileges to others.
GRANT ALL PRIVILEGES ON *.* TO 'remote'@'%' WITH GRANT OPTION;

-- Apply the privilege changes immediately.
-- This ensures the new user and permissions are active.
FLUSH PRIVILEGES;
EOF