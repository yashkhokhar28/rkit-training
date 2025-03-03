-- Drop the database if it already exists 
DROP DATABASE IF EXISTS EmployeeTaskManager;

-- Create the database
CREATE DATABASE EmployeeTaskManager;
USE EmployeeTaskManager;

-- Create USR01 without foreign key
CREATE TABLE USR01 (
    R01F01  INT PRIMARY KEY AUTO_INCREMENT COMMENT 'user_employee_id',
    R01F02  VARCHAR(50) UNIQUE NOT NULL COMMENT 'username',
    R01F03  VARCHAR(255) NOT NULL COMMENT 'password_hash',
    R01F04  ENUM('Admin', 'Manager', 'Employee') NOT NULL DEFAULT 'Employee' COMMENT 'role',
    R01F05  VARCHAR(50) NOT NULL COMMENT 'first_name',
    R01F06  VARCHAR(50) NOT NULL COMMENT 'last_name',
    R01F07  VARCHAR(100) UNIQUE NOT NULL COMMENT 'email',
    R01F08  INT NOT NULL COMMENT 'department_id',
    R01F09  DATE NOT NULL COMMENT 'hire_date',
    R01F10  TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    R01F11  TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at'
);

-- Create DPT01 without foreign key
CREATE TABLE DPT01 (
    T01F01  INT PRIMARY KEY AUTO_INCREMENT COMMENT 'department_id',
    T01F02  VARCHAR(100) NOT NULL COMMENT 'name',
    T01F03  INT NOT NULL COMMENT 'manager_id',
    T01F04  TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    T01F05  TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at'
);

-- Create TSK01 without foreign keys
CREATE TABLE TSK01 (
    K01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'task_id',
    K01F02 VARCHAR(200) NOT NULL COMMENT 'title',
    K01F03 TEXT COMMENT 'description',
    K01F04 INT NOT NULL COMMENT 'assigned_to',
    K01F05 INT NOT NULL COMMENT 'department_id',
    K01F06 ENUM('Pending', 'InProgress', 'Completed', 'Overdue') DEFAULT 'Pending' COMMENT 'status',
    K01F07 ENUM('Low', 'Medium', 'High') DEFAULT 'Medium' COMMENT 'priority',
    K01F08 DATE COMMENT 'due_date',
    K01F09 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    K01F10 TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at'
);

-- Add foreign key constraints
ALTER TABLE USR01
ADD FOREIGN KEY (R01F08) REFERENCES DPT01(T01F01);

ALTER TABLE DPT01
ADD FOREIGN KEY (T01F03) REFERENCES USR01(R01F01);

ALTER TABLE TSK01
ADD FOREIGN KEY (K01F04) REFERENCES USR01(R01F01),
ADD FOREIGN KEY (K01F05) REFERENCES DPT01(T01F01);

-- Insert dummy record into DPT01
INSERT INTO DPT01 (T01F02, T01F03) VALUES
('IT Department', 1); -- T01F03 (manager_id) will be updated after USR01 insert

-- Insert dummy record into USR01
INSERT INTO USR01 (R01F02, R01F03, R01F04, R01F05, R01F06, R01F07, R01F08, R01F09) VALUES
('john.doe', 'hashed_password_123', 'Admin', 'John', 'Doe', 'john.doe@example.com', 1, '2023-01-01');

-- Update DPT01 to set correct manager_id after USR01 insert
UPDATE DPT01 SET T01F03 = 1 WHERE T01F01 = 1;

-- Insert dummy record into TSK01
INSERT INTO TSK01 (K01F02, K01F03, K01F04, K01F05, K01F06, K01F07, K01F08) VALUES
('Setup Server', 'Set up a new server for the IT department', 1, 1, 'Pending', 'High', '2023-12-31');