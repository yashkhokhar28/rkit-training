#!/bin/bash

for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Connect to the database and apply VACUUM FULL on the users table
  psql -U postgres -d $DB_NAME -c "VACUUM FULL users;"

  echo "VACUUM FULL applied on users table in $DB_NAME"
done