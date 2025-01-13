DELIMITER //

CREATE PROCEDURE CreateDatabasesAndTables()
BEGIN
    DECLARE i INT DEFAULT 1;
    WHILE i <= 100 DO
        SET @db_name = CONCAT('test_db_', i);
        SET @create_db = CONCAT('CREATE DATABASE IF NOT EXISTS ', @db_name);
        PREPARE stmt1 FROM @create_db;
        EXECUTE stmt1;
        DEALLOCATE PREPARE stmt1;

        SET @create_table = CONCAT(
            'CREATE TABLE ', @db_name, '.users (',
            'Name VARCHAR(100), Sex VARCHAR(10), Age INT, Height FLOAT, Weight FLOAT, ',
            'Team VARCHAR(100), Year INT, Season VARCHAR(10), Host_City VARCHAR(100), Host_Country VARCHAR(100), ',
            'Sport VARCHAR(100), Event VARCHAR(200), GDP_Per_Capita_Constant_LCU_Value FLOAT, ',
            'Cereal_yield_kg_per_hectare_Value FLOAT, Military_expenditure_current_LCU_Value FLOAT, ',
            'Tax_revenue_current_LCU_Value FLOAT, Expense_current_LCU_Value FLOAT, ',
            'Central_government_debt_total_current_LCU_Value FLOAT, Representing_Host VARCHAR(10), ',
            'Avg_Temp FLOAT, Medal VARCHAR(50), Medal_Binary INT)'
        );

        PREPARE stmt2 FROM @create_table;
        EXECUTE stmt2;
        DEALLOCATE PREPARE stmt2;

        SET i = i + 1;
    END WHILE;
END //

DELIMITER ;

CALL CreateDatabasesAndTables();
SET GLOBAL local_infile = 1;
SHOW VARIABLES LIKE 'local_infile';

SHOW VARIABLES LIKE 'secure_file_priv';
use test_db_2;
select count(*) from users;