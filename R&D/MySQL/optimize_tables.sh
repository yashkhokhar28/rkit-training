#!/bin/bash

# Loop through all 100 databases
for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Run OPTIMIZE TABLE command on the 'users' table
  mysql -D $DB_NAME -e "
    OPTIMIZE TABLE users;
  "

  echo "Optimized 'users' table in $DB_NAME"
done