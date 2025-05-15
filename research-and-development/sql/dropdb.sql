DELIMITER $$

CREATE PROCEDURE drop_test_databases()
BEGIN
    DECLARE db_idx INT DEFAULT 1;
    DECLARE db_name VARCHAR(64);
    DECLARE query_text TEXT;

    WHILE db_idx <= 10 DO
        SET db_name = CONCAT('test', db_idx);
        SET @query_text = CONCAT('DROP DATABASE IF EXISTS ', db_name, ';');
        PREPARE stmt FROM @query_text;
        EXECUTE stmt;
        DEALLOCATE PREPARE stmt;
        SET db_idx = db_idx + 1;
    END WHILE;
END $$

DELIMITER ;
