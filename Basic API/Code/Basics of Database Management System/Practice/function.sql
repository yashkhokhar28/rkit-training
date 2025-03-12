DELIMITER $$

CREATE FUNCTION GetEmployeeCountByRole(p_R01F04 VARCHAR(30)) 
RETURNS INT 
DETERMINISTIC
BEGIN
    DECLARE emp_count INT;
    SELECT COUNT(*) INTO emp_count FROM USR01 WHERE R01F04 = p_R01F04;
    RETURN emp_count;
END $$

DELIMITER ;

DROP FUNCTION IF EXISTS GetEmployeeCountByRole;


SELECT GetEmployeeCountByRole('Employee');
SELECT GetEmployeeCountByRole('Manager');
