CREATE DATABASE EmployeeDB;
USE EmployeeDB;

CREATE TABLE EMP01 (
    P01F01 INT PRIMARY KEY AUTO_INCREMENT,
    P01F02 VARCHAR(255) NOT NULL,
    P01F03 INT NOT NULL,
    P01F04 VARCHAR(10) NOT NULL,
    P01F05 DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    P01F06 DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

INSERT INTO EMP01 (P01F02, P01F03, P01F04)
VALUES 
('Tony Stark', 45, 'Adult'),
('Peter Parker', 16, 'Minor'),
('Steve Rogers', 101, 'Adult'),
('Natasha Romanoff', 35, 'Adult'),
('Bruce Banner', 40, 'Adult'),
('Wanda Maximoff', 25, 'Adult'),
('Loki Laufeyson', 17, 'Minor'),
('Thor Odinson', 1500, 'Adult'),
('Clint Barton', 35, 'Adult'),
('Shuri', 18, 'Adult');

SELECT * FROM EMP01


