#!/bin/bash

# SQL command to truncate the users table
TRUNCATE_TABLE_QUERY="TRUNCATE TABLE users;"

# Loop to truncate the `users` table in each database
for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Execute the TRUNCATE TABLE command
  mysql -D $DB_NAME -e "$TRUNCATE_TABLE_QUERY"

  echo "Table 'users' truncated in $DB_NAME"
done