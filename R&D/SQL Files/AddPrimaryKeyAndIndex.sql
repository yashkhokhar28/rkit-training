DELIMITER $$

DROP PROCEDURE IF EXISTS AddPrimaryKeyAndIndex $$

CREATE PROCEDURE AddPrimaryKeyAndIndex()
BEGIN
    DECLARE i INT DEFAULT 1;
    DECLARE table_name VARCHAR(100);
    DECLARE sql_query VARCHAR(255);
    DECLARE pk_exists INT DEFAULT 0;
    DECLARE index_exists INT DEFAULT 0;
    
    WHILE i <= 100 DO
        -- Set the table name dynamically
        SET table_name = CONCAT('orders_', i);
        
        -- Check if table exists before proceeding
        SET @table_check = (SELECT COUNT(*) FROM information_schema.tables 
                            WHERE table_schema = 'test_db_1' AND table_name = table_name);
                            
        IF @table_check = 0 THEN
            SELECT CONCAT('Table does not exist: ', table_name) AS DebugMessage;
        ELSE
            -- Check if Primary Key exists
            SET @sql = CONCAT(
                "SELECT COUNT(*) INTO @pk_exists FROM information_schema.table_constraints ",
                "WHERE table_schema = 'test_db_1' AND table_name = '", table_name, "' ",
                "AND constraint_type = 'PRIMARY KEY';"
            );
            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;

            -- Debug output
            SELECT table_name AS TableName, @pk_exists AS PrimaryKeyExists;

            -- If no Primary Key, add it
            IF @pk_exists = 0 THEN
                SET @sql_query = CONCAT("ALTER TABLE test_db_1.", table_name, " ADD PRIMARY KEY (`index`);");
                PREPARE stmt FROM @sql_query;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                SELECT CONCAT('Added PRIMARY KEY to: ', table_name) AS DebugMessage;
            END IF;

            -- Check if Index exists
            SET @sql = CONCAT(
                "SELECT COUNT(*) INTO @index_exists FROM information_schema.statistics ",
                "WHERE table_schema = 'test_db_1' AND table_name = '", table_name, "' ",
                "AND index_name = 'idx_index';"
            );
            PREPARE stmt FROM @sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;

            -- Debug output
            SELECT table_name AS TableName, @index_exists AS IndexExists;

            -- If no Index, create it
            IF @index_exists = 0 THEN
                SET @sql_query = CONCAT("CREATE INDEX idx_index ON test_db_1.", table_name, "(`index`);");
                PREPARE stmt FROM @sql_query;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                SELECT CONCAT('Added INDEX to: ', table_name) AS DebugMessage;
            END IF;
        END IF;

        -- Move to the next table
        SET i = i + 1;
    END WHILE;
END $$

DELIMITER ;





-- Execute the procedure
CALL AddPrimaryKeyAndIndex();
DROP PROCEDURE AddPrimaryKeyAndIndex;

