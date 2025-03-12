-- AFTER Trigger – Executes after the triggering event.

CREATE TABLE EmployeeAudit (
    AuditID INT AUTO_INCREMENT PRIMARY KEY,
    EmployeeID INT NOT NULL,
    Action VARCHAR(10) NOT NULL CHECK (Action IN ('INSERT', 'UPDATE', 'DELETE')),
    ActionDate DATETIME NOT NULL DEFAULT NOW(),
    FOREIGN KEY (EmployeeID) REFERENCES USR01(R01F01) ON DELETE CASCADE
);

SELECT * FROM EmployeeAudit;

DELIMITER $$

CREATE TRIGGER after_employee_insert
AFTER INSERT ON USR01
FOR EACH ROW
BEGIN
	INSERT INTO EmployeeAudit(EmployeeID, Action, ActionDate)
    VALUES (NEW.R01F01, 'INSERT', NOW());
END $$

DELIMITER ;


-- BEFORE Trigger – Executes before the triggering event.

DELIMITER $$

CREATE TRIGGER before_employee_update
BEFORE UPDATE ON USR01
FOR EACH ROW
BEGIN
    SET NEW.R01F11 = NOW();
END $$

DELIMITER ;
