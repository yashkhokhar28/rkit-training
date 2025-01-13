#!/bin/bash

# Path to your CSV file
CSV_FILE="/Users/yashkhokhar/Desktop/df_final_features.csv"

# Loop to load data into the `users` table in each database
for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Load the CSV file into the existing `users` table
  psql -U postgres -d $DB_NAME -c "\COPY users FROM '$CSV_FILE' DELIMITER ',' CSV HEADER"

  echo "Data loaded into $DB_NAME"
done