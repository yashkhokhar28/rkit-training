DELIMITER //

CREATE PROCEDURE create_test_dbs_and_tables()
BEGIN
    DECLARE db_num INT DEFAULT 1;
    DECLARE table_num INT;
    DECLARE i INT;

    WHILE db_num <= 10 DO
        SET @dbname = CONCAT('test', db_num);
        SET @create_db_sql = CONCAT('CREATE DATABASE IF NOT EXISTS ', @dbname);
        PREPARE stmt FROM @create_db_sql;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;

        SET table_num = 1;
        WHILE table_num <= 10 DO
            SET @tablename = CONCAT('testdemo', table_num);
            SET @create_table_sql = CONCAT('
                CREATE TABLE IF NOT EXISTS ', @dbname, '.', @tablename, ' (
                    id INT AUTO_INCREMENT PRIMARY KEY,
                    name VARCHAR(50)
                )');
            PREPARE stmt FROM @create_table_sql;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;

            SET i = 1;
            WHILE i <= 50 DO
                SET @insert_sql = CONCAT('
                    INSERT INTO ', @dbname, '.', @tablename, ' (name)
                    VALUES (''Name_', i, ''')');
                PREPARE stmt FROM @insert_sql;
                EXECUTE stmt;
                DEALLOCATE PREPARE stmt;
                SET i = i + 1;
            END WHILE;

            SET table_num = table_num + 1;
        END WHILE;

        SET db_num = db_num + 1;
    END WHILE;
END //

DELIMITER ;
