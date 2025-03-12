DELIMITER $$

CREATE PROCEDURE ProcessEmployees()
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE emp_id INT;
    DECLARE emp_name VARCHAR(255);

    -- Declare Cursor
    DECLARE emp_cursor CURSOR FOR SELECT R01F01, R01F02 FROM USR01;
    -- Declare Handler
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;

    -- Open Cursor
    OPEN emp_cursor;

    read_loop: LOOP
        FETCH emp_cursor INTO emp_id, emp_name;
        IF done THEN 
            LEAVE read_loop;
        END IF;

        -- Process each row (Example: Printing using SELECT)
        SELECT emp_id, emp_name;
    END LOOP;

    -- Close Cursor
    CLOSE emp_cursor;
END $$

DELIMITER ;

CALL ProcessEmployees();
