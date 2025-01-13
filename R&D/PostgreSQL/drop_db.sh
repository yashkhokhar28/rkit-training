#!/bin/bash

for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Drop the database if it exists
  psql -U postgres -c "DROP DATABASE IF EXISTS $DB_NAME;"

  echo "Dropped database $DB_NAME"
done