#!/bin/bash

# Path to your CSV file
CSV_FILE="/Users/yashkhokhar/Desktop/df_final_features.csv"

# Loop to load data into the `users` table in each database
for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Load the CSV file into the existing `users` table
  mysql --local-infile=1 -D $DB_NAME -e "
    LOAD DATA LOCAL INFILE '$CSV_FILE'
    INTO TABLE users
    FIELDS TERMINATED BY ','
    LINES TERMINATED BY '\n'
    IGNORE 1 ROWS;
  "

  echo "Data loaded into $DB_NAME"
done