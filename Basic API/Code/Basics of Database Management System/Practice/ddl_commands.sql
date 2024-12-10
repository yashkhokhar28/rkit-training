-- CREATE
-- CREATE DATABASE
CREATE DATABASE location_db;

-- CREATE TABLE
CREATE TABLE Country (
    CountryID INT AUTO_INCREMENT PRIMARY KEY,
    CountryName VARCHAR(15) NOT NULL,
    CountryCode VARCHAR(7) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- CREATE INDEX
CREATE INDEX idx_CityName ON City(CityName);

-- CREATE VIEW
CREATE VIEW HighGPAStudents AS
SELECT FirstName, LastName, GPA
FROM Student
WHERE GPA > 8;


-- ALTER
-- Add a Column
ALTER TABLE Student ADD MiddleName VARCHAR(50) NOT NULL;

-- Modify a Column
ALTER TABLE Student MODIFY PhoneNumber VARCHAR(15);

-- Rename a Column
ALTER TABLE Student CHANGE Address FullAddress VARCHAR(100);

-- Drop a Column
ALTER TABLE Student DROP COLUMN IsFullTime;

-- Add a Constraint
ALTER TABLE Student ADD CONSTRAINT fk_country FOREIGN KEY (CountryID) REFERENCES Country(CountryID);

-- Rename a Table
ALTER TABLE Student RENAME TO Students;


-- DROP
-- Drop a Table
DROP TABLE Students;

-- Drop a Database
DROP DATABASE location_db;

-- Drop an Index
DROP INDEX idx_CityName ON City;

-- Drop a View
DROP VIEW HighGPAStudents;

-- TRUNCATE-- 
-- Truncate a Table
TRUNCATE TABLE Student;