-- COUNT(): Count the number of rows in a table or a group.
SELECT 
	COUNT(*) AS TotalStudents
FROM 
	Student;
    
-- SUM(): Calculate the sum of a column.    
SELECT 
	SUM(GPA) AS TotalGPA
FROM 
	Student;
    
-- AVG(): Calculate the average of a numeric column.
SELECT 
	AVG(GPA) AS AverageGPA
FROM 
	Student;

-- MIN(): Find the minimum value in a column.
SELECT 
	MIN(GPA) AS MinGPA
FROM 
	Student;

-- MAX(): Find the maximum value in a column.
SELECT 
	MAX(GPA) AS MaxGPA
FROM 
	Student;
    
-- GROUP_CONCAT() (MySQL): Concatenate values from multiple rows into a single string.
SELECT 
	GROUP_CONCAT(CityName) AS Cities
FROM 
	City
WHERE 
	StateID = 1;