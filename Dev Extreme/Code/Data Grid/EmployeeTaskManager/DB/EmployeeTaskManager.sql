CREATE DATABASE EmployeeTaskManager;
USE EmployeeTaskManager;
DROP DATABASE EmployeeTaskManager;

-- Departments table
CREATE TABLE DPT01 (
    T01F01 	INT PRIMARY KEY AUTO_INCREMENT COMMENT 'department_id',
    T01F02 	VARCHAR(100) NOT NULL COMMENT 'name',
    T01F03 	INT COMMENT 'manager_id',
    T01F04 	TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    T01F05 	TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at'
);

-- Employees table
CREATE TABLE EMP01 (
    P01F01	INT PRIMARY KEY AUTO_INCREMENT COMMENT 'employee_id',
    P01F02 	VARCHAR(50) NOT NULL COMMENT 'first_name',
    P01F03 	VARCHAR(50) NOT NULL COMMENT 'last_name',
    P01F04 	VARCHAR(100) UNIQUE NOT NULL COMMENT 'email',
    P01F05 	VARCHAR(50) COMMENT 'role',
    P01F06 	INT COMMENT 'department_id',
    P01F07 	DATE COMMENT 'hire_date',
    P01F08 	TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'created_at',
    P01F09 	TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at',
    FOREIGN KEY (P01F06) REFERENCES DPT01(T01F01)
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
    K01F010 TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'updated_at',
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
-- Note: T01F03 (manager_id) in DPT01 will be updated later after employees are inserted
INSERT INTO EMP01 (P01F02, P01F03, P01F04, P01F05, P01F06, P01F07) VALUES
('Alpesh', 'Patel', 'alpesh.patel@example.com', 'Manager', 1, '2022-01-15'),
('Bhavna', 'Shah', 'bhavna.shah@example.com', 'Developer', 1, '2022-03-10'),
('Chetan', 'Desai', 'chetan.desai@example.com', 'Developer', 1, '2022-04-01'),
('Dipika', 'Joshi', 'dipika.joshi@example.com', 'HR Lead', 2, '2021-11-20'),
('Esha', 'Mehta', 'esha.mehta@example.com', 'Recruiter', 2, '2023-01-05'),
('Fenil', 'Parmar', 'fenil.parmar@example.com', 'Sales Lead', 3, '2022-06-15'),
('Gita', 'Raval', 'gita.raval@example.com', 'Sales Rep', 3, '2023-02-10');

-- Update DPT01 to set manager_id (T01F03) after employees are inserted
UPDATE DPT01 SET T01F03 = 1 WHERE T01F01 = 1; -- Alpesh Patel manages Engineering
UPDATE DPT01 SET T01F03 = 4 WHERE T01F01 = 2; -- Dipika Joshi manages HR
UPDATE DPT01 SET T01F03 = 6 WHERE T01F01 = 3; -- Fenil Parmar manages Sales

-- Insert data into TSK01 (Tasks)
INSERT INTO TSK01 (K01F02, K01F03, K01F04, K01F05, K01F06, K01F07, K01F08) VALUES
('Develop Login Module', 'Create authentication system', 2, 1, 'InProgress', 'High', '2025-03-15'),
('Fix Bugs in Dashboard', 'Resolve UI issues', 3, 1, 'Pending', 'Medium', '2025-03-20'),
('Hire New Developer', 'Interview candidates', 5, 2, 'Completed', 'Medium', '2025-02-20'),
('Prepare Sales Report', 'Analyze Q1 sales data', 7, 3, 'Overdue', 'High', '2025-02-25'),
('Unit Test API', 'Write test cases for endpoints', 2, 1, 'Pending', 'Low', '2025-03-25'),
('Onboard New Hire', 'Set up workstation and training', 4, 2, 'InProgress', 'Medium', '2025-03-10'),
('Client Follow-Up', 'Call potential leads', 6, 3, 'Completed', 'High', '2025-02-15');