
-- PR_Employee_SelectAll
DELIMITER $$
CREATE PROCEDURE PR_Employee_SelectAll()
BEGIN
	SELECT
		R01F01,
        R01F02,
        R01F03,
        R01F04,
        R01F05,
        R01F06,
        R01F07,
        R01F08,
        R01F09,
        R01F10,
        R01F11
	FROM
		USR01;
END $$

DELIMITER ; 

CALL PR_Employee_SelectAll(); 

-- PR_Employee_SelectByID
DELIMITER $$
CREATE PROCEDURE PR_Employee_SelectByID(IN p_R01F01 INT)
BEGIN
	SELECT
		R01F01,
        R01F02,
        R01F03,
        R01F04,
        R01F05,
        R01F06,
        R01F07,
        R01F08,
        R01F09,
        R01F10,
        R01F11
	FROM
		USR01
	WHERE R01F01 = p_R01F01;
END $$

DELIMITER ; 

CALL PR_Employee_SelectByID(5); 

-- PR_Employee_Count
DELIMITER $$
CREATE PROCEDURE PR_Employee_Count(IN p_R01F04 VARCHAR(20), OUT p_EmpCount INT)
BEGIN
	SELECT
		COUNT(R01F01)
        INTO p_EmpCount
	FROM
		USR01
	WHERE R01F04 = p_R01F04;
END $$

DELIMITER ; 

CALL PR_Employee_Count('Employee', @total_count);
SELECT @total_count;

-- PR_Employee_Insert
DELIMITER $$
CREATE PROCEDURE PR_Employee_Insert(p_R01F02 VARCHAR(50),p_R01F03 VARCHAR(255),p_R01F04 enum('Admin','Manager','Employee'),p_R01F05 VARCHAR(50),p_R01F06 VARCHAR(50),p_R01F07 VARCHAR(50),p_R01F08 INT,p_R01F09 DATE)
BEGIN
	INSERT INTO USR01
    (
		R01F02,
        R01F03,
        R01F04,
        R01F05,
        R01F06,
        R01F07,
        R01F08,
        R01F09
    )
    VALUES
    (
		p_R01F02,
        p_R01F03,
        p_R01F04,
        p_R01F05,
        p_R01F06,
        p_R01F07,
        p_R01F08,
        p_R01F09
    );
END $$

DELIMITER ; 

CALL PR_Employee_Insert('yashkhokhar','password','Employee','Yash','Khokhar','yash.khokhar@gmail.com',4,'2023-01-03');
CALL PR_Employee_Insert('yashkhokhar123','password','Employee','Yash','Khokhar','yash.khokhar123@gmail.com',4,'2023-01-03');

DELIMITER $$
CREATE PROCEDURE PR_Employee_Update(p_R01F01 INT,p_R01F02 VARCHAR(50),p_R01F03 VARCHAR(255),p_R01F04 enum('Admin','Manager','Employee'),p_R01F05 VARCHAR(50),p_R01F06 VARCHAR(50),p_R01F07 VARCHAR(50),p_R01F08 INT,p_R01F09 DATE)
BEGIN
	UPDATE USR01
    SET 
		R01F02 = p_R01F02, 
        R01F03 = p_R01F03,
        R01F04 = p_R01F04,
        R01F05 = p_R01F05,
        R01F06 = p_R01F06,
        R01F07 = p_R01F07,
        R01F08 = p_R01F08,
        R01F09 = p_R01F09
	WHERE R01F01 = p_R01F01;
END $$

DELIMITER ; 

CALL PR_Employee_Update(1,'yashkhokhar1','password','Employee','Yash','Khokhar','yash.khokhar@gmail.com1',4,'2023-01-03');