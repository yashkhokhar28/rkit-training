#!/bin/bash

for i in {1..100}
do
  DB_NAME="test_db_$i"
  
  # Create the database if it doesn't exist
  psql -U postgres -c "CREATE DATABASE $DB_NAME;"

  # Connect to the database and create the table
  psql -U postgres -d $DB_NAME -c "
    CREATE TABLE IF NOT EXISTS users (
      Name TEXT,                                    -- TEXT for unlimited length
      Sex TEXT,                                     -- TEXT for unlimited length
      Age TEXT,                                   -- BIGINT for larger integer values
      Height DOUBLE PRECISION,                      -- DOUBLE PRECISION for high precision floating-point
      Weight DOUBLE PRECISION,                      -- DOUBLE PRECISION for high precision floating-point
      Team TEXT,                                    -- TEXT for unlimited length
      Year BIGINT,                                  -- BIGINT for larger integer values
      Season TEXT,                                  -- TEXT for unlimited length
      Host_City TEXT,                               -- TEXT for unlimited length
      Host_Country TEXT,                            -- TEXT for unlimited length
      Sport TEXT,                                   -- TEXT for unlimited length
      Event TEXT,                                   -- TEXT for unlimited length
      GDP_Per_Capita_Constant_LCU_Value DOUBLE PRECISION, -- DOUBLE PRECISION for high precision floating-point
      Cereal_yield_kg_per_hectare_Value DOUBLE PRECISION, -- DOUBLE PRECISION for high precision floating-point
      Military_expenditure_current_LCU_Value DOUBLE PRECISION, -- DOUBLE PRECISION for high precision floating-point
      Tax_revenue_current_LCU_Value DOUBLE PRECISION, -- DOUBLE PRECISION for high precision floating-point
      Expense_current_LCU_Value DOUBLE PRECISION,   -- DOUBLE PRECISION for high precision floating-point
      Central_government_debt_total_current_LCU_Value DOUBLE PRECISION, -- DOUBLE PRECISION for high precision floating-point
      Representing_Host TEXT,                       -- TEXT for unlimited length
      Avg_Temp TEXT,                    -- DOUBLE PRECISION for high precision floating-point
      Medal TEXT,                                   -- TEXT for unlimited length
      Medal_Binary BIGINT                           -- BIGINT for binary values (0/1)
    );
  "

  echo "Database $DB_NAME and users table created."
done