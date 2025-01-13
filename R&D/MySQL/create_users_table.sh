#!/bin/bash

# SQL command to create the users table
CREATE_TABLE_QUERY="
CREATE TABLE IF NOT EXISTS users (
    Name VARCHAR(100),
    Sex VARCHAR(10),
    Age INT,
    Height FLOAT,
    Weight FLOAT,
    Team VARCHAR(100),
    Year INT,
    Season VARCHAR(10),
    Host_City VARCHAR(100),
    Host_Country VARCHAR(100),
    Sport VARCHAR(100),
    Event VARCHAR(200),
    GDP_Per_Capita_Constant_LCU_Value FLOAT,
    Cereal_yield_kg_per_hectare_Value FLOAT,
    Military_expenditure_current_LCU_Value FLOAT,
    Tax_revenue_current_LCU_Value FLOAT,
    Expense_current_LCU_Value FLOAT,
    Central_government_debt_total_current_LCU_Value FLOAT,
    Representing_Host VARCHAR(10),
    Avg_Temp FLOAT,
    Medal VARCHAR(50),
    Medal_Binary INT
);
"

# Loop to create the `users` table in each database
for i in {1..100}
do
  DB_NAME="test_db_$i"

  # Execute the CREATE TABLE command
  mysql -D $DB_NAME -e "$CREATE_TABLE_QUERY"

  echo "Table 'users' created in $DB_NAME"
done