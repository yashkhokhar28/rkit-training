#!/bin/bash

# Loop through all 100 databases
for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Calculate total rows in the users table
  TOTAL_ROWS=$(mysql -u root -D $DB_NAME -Bse "SELECT COUNT(*) FROM users;")

  # Calculate half of the rows
  HALF_ROWS=$((TOTAL_ROWS / 2))

  # Delete half of the records
  mysql -D $DB_NAME -e "
    DELETE FROM users
    LIMIT $HALF_ROWS;
  "

  echo "Deleted $HALF_ROWS rows from $DB_NAME"
done