#!/bin/bash

for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Connect to the database and delete half of the records from the `users` table
  psql -U postgres -d $DB_NAME -c "
    DELETE FROM users
    WHERE ctid IN (
      SELECT ctid FROM users
      LIMIT (SELECT COUNT(*) / 2 FROM users)
    );
  "

  echo "Deleted half of the records from $DB_NAME"
done