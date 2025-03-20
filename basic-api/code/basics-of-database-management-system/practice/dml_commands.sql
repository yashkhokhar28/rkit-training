-- INSERT
-- Insert Single Row
INSERT INTO Country (CountryName, CountryCode) VALUES ('India', 'IN');

-- Insert Multiple Rows
INSERT INTO Country (CountryName, CountryCode) VALUES 
('India', 'IN'),
('Australia','AUS');

-- Insert Data From Another Table
INSERT INTO Student (CountryID, IsFullTime)
SELECT CountryID,TRUE FROM Country WHERE CountryID = 2;

-- UPDATE
-- Update Single Column
UPDATE Student
SET IsFullTime = TRUE
WHERE StudentID = 1;

-- Update Multiple Columns
UPDATE Student
SET IsFullTime = TRUE, Gender = 'Female'
WHERE FirstName = 'John Doe';

-- Update With a Condition
UPDATE Student
SET GPA = GPA - 1
WHERE StudentID = 1;


-- DELETE
-- Delete Specific Rows
DELETE FROM Student 
WHERE FirstName = 'Liam White';

-- Delete All Rows From a Table
DELETE FROM Student;

-- Delete With a Condition
DELETE FROM Student 
WHERE StudentID = 4;