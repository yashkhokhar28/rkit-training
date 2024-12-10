-- Basic UNION (Remove Duplicates)
-- Combine the first names of students and cities (removing duplicates).
SELECT 
	FirstName AS Name
FROM 
	Student
UNION
SELECT 
	CityName AS Name
FROM 
	City;
    
-- UNION ALL (Include Duplicates)
-- Combine the first names of students and cities (including duplicates).
SELECT 
	FirstName AS Name
FROM 
	Student
UNION ALL
SELECT 
	CityName AS Name
FROM 
	City;
    
-- Union for Different Data Types
-- Combine students’ FirstName and their PhoneNumber in a single list.
SELECT 
	FirstName AS ContactInfo
FROM 
	Student
UNION
SELECT 
	PhoneNumber AS ContactInfo
FROM 
	Student;
    
-- UNION Across Multiple Tables
-- Combine the student names from the Student table and the country names from the Country table.
SELECT 
	FirstName AS Name
FROM 
	Student
UNION
SELECT 
	CountryName AS Name
FROM 
	Country;
    
-- Using UNION to Combine Results from Different States
-- Combine students from Gujarat (StateID = 1) and Maharashtra (StateID = 2) into one list.    
SELECT 
	FirstName, StateName
FROM 
	Student
JOIN 
	State ON Student.StateID = State.StateID
WHERE 
	State.StateID = 1
UNION
SELECT 
	FirstName, StateName
FROM 
	Student
JOIN 
	State ON Student.StateID = State.StateID
WHERE 
	State.StateID = 2;