#!/bin/bash

# MySQL credentials
USER="root"

# Loop through databases test1 to test10
for db_num in {1..10}; do
    DB="test$db_num"
    echo "Creating database: $DB"
    mysql -u "$USER" -e "CREATE DATABASE IF NOT EXISTS $DB;"
    
    # Loop through tables testdemo1 to testdemo10
    for table_num in {1..10}; do
        TABLE="testdemo$table_num"
        echo "Creating table: $TABLE in $DB"
        
        # Create table
        mysql -u "$USER" "$DB" -e "
            CREATE TABLE IF NOT EXISTS $TABLE (
                id INT AUTO_INCREMENT PRIMARY KEY,
                name VARCHAR(50)
            );
        "
        
        # Insert 50 dummy rows
        INSERT_QUERY="INSERT INTO $TABLE (name) VALUES"
        for i in $(seq 1 50); do
            INSERT_QUERY+=" ('Name_$i')"
            if [ $i -lt 50 ]; then
                INSERT_QUERY+=","
            else
                INSERT_QUERY+=";"
            fi
        done
        
        mysql -u "$USER" "$DB" -e "$INSERT_QUERY"
    done
done