#!/bin/bash

for i in {1..100}
do
  DB_NAME="test_db_$i"
  
  echo "Truncating table in $DB_NAME..."

  # Connect to each database and truncate the table
  psql -U postgres -d "$DB_NAME" -c "TRUNCATE TABLE users CASCADE;"

  echo "Table truncated in $DB_NAME."
done

echo "All tables truncated successfully!"