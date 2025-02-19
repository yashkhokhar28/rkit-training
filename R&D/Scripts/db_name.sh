#!/bin/bash

# Check if the database name is provided
if [ -z "$1" ]; then
    echo "Usage: $0 <database_name>"
    exit 1
fi

DB_NAME=$1
OUTPUT_FILE="${DB_NAME}_tables.txt"

# Fetch table names from the database
mysql -N -e "SHOW TABLES FROM $DB_NAME;" >"$OUTPUT_FILE"

if [ $? -eq 0 ]; then
    echo "Table list saved to $OUTPUT_FILE"
else
    echo "Error retrieving tables from database: $DB_NAME"
fi
