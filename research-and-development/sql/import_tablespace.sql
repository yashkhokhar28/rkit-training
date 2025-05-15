DELIMITER $$

CREATE PROCEDURE import_tablespaces()
BEGIN
    DECLARE db_idx INT DEFAULT 1;
    DECLARE tbl_idx INT;
    DECLARE db_name VARCHAR(64);
    DECLARE tbl_name VARCHAR(64);
    DECLARE query_text TEXT;

    WHILE db_idx <= 10 DO
        SET db_name = CONCAT('test', db_idx);
        SET tbl_idx = 1;
        WHILE tbl_idx <= 10 DO
            SET tbl_name = CONCAT('testdemo', tbl_idx);
            SET @query_text = CONCAT('ALTER TABLE ', db_name, '.', tbl_name, ' IMPORT TABLESPACE;');
            PREPARE stmt FROM @query_text;
            EXECUTE stmt;
            DEALLOCATE PREPARE stmt;
            SET tbl_idx = tbl_idx + 1;
        END WHILE;
        SET db_idx = db_idx + 1;
    END WHILE;
END $$

DELIMITER ;
