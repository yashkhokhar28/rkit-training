-- Drop the database if it already exists (optional, use carefully)
DROP DATABASE IF EXISTS EmployeeTaskManager;

-- Create the database
CREATE DATABASE EmployeeTaskManager;
USE EmployeeTaskManager;

-- Users table for authentication
CREATE TABLE USR01 (
    R01F01  INT PRIMARY KEY AUTO_INCREMENT COMMENT 'user_id',
    R01F02  VARCHAR(50) UNIQUE NOT NULL COMMENT 'username',
    R01F03  VARCHAR(255) NOT NULL COMMENT 'password_hash',
    R01F04  ENUM('Admin', 'Manager', 'Employee') NOT NULL DEFAULT 'Employee' COMMENT 'role',
    R01F05  INT COMMENT 'employee_id',
    R01F06  TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    R01F07  TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at',
    FOREIGN KEY (R01F05) REFERENCES EMP01(P01F01) ON DELETE CASCADE
);

-- Departments table
CREATE TABLE DPT01 (
    T01F01  INT PRIMARY KEY AUTO_INCREMENT COMMENT 'department_id',
    T01F02  VARCHAR(100) NOT NULL COMMENT 'name',
    T01F03  INT COMMENT 'manager_id',
    T01F04  TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    T01F05  TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at'
);

-- Employees table
CREATE TABLE EMP01 (
    P01F01  INT PRIMARY KEY AUTO_INCREMENT COMMENT 'employee_id',
    P01F02  VARCHAR(50) NOT NULL COMMENT 'first_name',
    P01F03  VARCHAR(50) NOT NULL COMMENT 'last_name',
    P01F04  VARCHAR(100) UNIQUE NOT NULL COMMENT 'email',
    P01F05  INT COMMENT 'department_id',
    P01F06  DATE COMMENT 'hire_date',
    P01F07  TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    P01F08  TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at',
    FOREIGN KEY (P01F05) REFERENCES DPT01(T01F01)
);

-- Tasks table
CREATE TABLE TSK01 (
    K01F01 INT PRIMARY KEY AUTO_INCREMENT COMMENT 'task_id',
    K01F02 VARCHAR(200) NOT NULL COMMENT 'title',
    K01F03 TEXT COMMENT 'description',
    K01F04 INT COMMENT 'assigned_to',
    K01F05 INT COMMENT 'department_id',
    K01F06 ENUM('Pending', 'InProgress', 'Completed', 'Overdue') DEFAULT 'Pending' COMMENT 'status',
    K01F07 ENUM('Low', 'Medium', 'High') DEFAULT 'Medium' COMMENT 'priority',
    K01F08 DATE COMMENT 'due_date',
    K01F09 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    K01F10 TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at',
    FOREIGN KEY (K01F04) REFERENCES EMP01(P01F01),
    FOREIGN KEY (K01F05) REFERENCES DPT01(T01F01)
);

-- Add foreign key for manager_id in departments (self-referencing)
ALTER TABLE DPT01
ADD FOREIGN KEY (T01F03) REFERENCES EMP01(P01F01);

-- Insert data into DPT01 (Departments)
INSERT INTO DPT01 (T01F02) VALUES
('Engineering'),
('Human Resources'),
('Sales');

-- Insert data into EMP01 (Employees)
INSERT INTO EMP01 (P01F02, P01F03, P01F04, P01F05, P01F06) VALUES
('Alpesh', 'Patel', 'alpesh.patel@example.com', 1, '2022-01-15'),
('Bhavna', 'Shah', 'bhavna.shah@example.com', 1, '2022-03-10'),
('Chetan', 'Desai', 'chetan.desai@example.com', 1, '2022-04-01'),
('Dipika', 'Joshi', 'dipika.joshi@example.com', 2, '2021-11-20'),
('Esha', 'Mehta', 'esha.mehta@example.com', 2, '2023-01-05'),
('Fenil', 'Parmar', 'fenil.parmar@example.com', 3, '2022-06-15'),
('Gita', 'Raval', 'gita.raval@example.com', 3, '2023-02-10');

-- Update DPT01 to set manager_id (T01F03) after employees are inserted
UPDATE DPT01 SET T01F03 = 1 WHERE T01F01 = 1; -- Alpesh Patel manages Engineering
UPDATE DPT01 SET T01F03 = 4 WHERE T01F01 = 2; -- Dipika Joshi manages HR
UPDATE DPT01 SET T01F03 = 6 WHERE T01F01 = 3; -- Fenil Parmar manages Sales

-- Insert data into USR01 (Users Table for Authentication)
INSERT INTO USR01 (R01F02, R01F03, R01F04, R01F05) VALUES
('alpesh.patel', 'hashed_password_1', 'Admin', 1),
('bhavna.shah', 'hashed_password_2', 'Employee', 2),
('chetan.desai', 'hashed_password_3', 'Employee', 3),
('dipika.joshi', 'hashed_password_4', 'Manager', 4),
('esha.mehta', 'hashed_password_5', 'Employee', 5),
('fenil.parmar', 'hashed_password_6', 'Manager', 6),
('gita.raval', 'hashed_password_7', 'Employee', 7);

-- Insert data into TSK01 (Tasks)
INSERT INTO TSK01 (K01F02, K01F03, K01F04, K01F05, K01F06, K01F07, K01F08) VALUES
('Develop Login Module', 'Create authentication system', 2, 1, 'InProgress', 'High', '2025-03-15'),
('Fix Bugs in Dashboard', 'Resolve UI issues', 3, 1, 'Pending', 'Medium', '2025-03-20'),
('Hire New Developer', 'Interview candidates', 5, 2, 'Completed', 'Medium', '2025-02-20'),
('Prepare Sales Report', 'Analyze Q1 sales data', 7, 3, 'Overdue', 'High', '2025-02-25'),
('Unit Test API', 'Write test cases for endpoints', 2, 1, 'Pending', 'Low', '2025-03-25'),
('Onboard New Hire', 'Set up workstation and training', 4, 2, 'InProgress', 'Medium', '2025-03-10'),
('Client Follow-Up', 'Call potential leads', 6, 3, 'Completed', 'High', '2025-02-15');
