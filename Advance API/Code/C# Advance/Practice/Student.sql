CREATE DATABASE ORMDB;
USE ORMDB;

CREATE TABLE stu01 (
    u01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT "StudentID",
    u01f02 VARCHAR(100) NOT NULL COMMENT "StudentName",
    u01f03 INT NOT NULL COMMENT "StudentAge"
);

INSERT INTO stu01 (u01f02, u01f03)
VALUES
    ('Alice Smith', 22),
    ('Bob Johnson', 24),
    ('Charlie Brown', 19);
    
SELECT * FROM stu01

