-- Find Students with Null CityID
SELECT 
	StudentID,
    FirstName,
    LastName,
    Address
FROM 
	Student
WHERE 
	CityID IS NULL;

-- Find States with CountryID Between 1 and 3
SELECT 
	StateID,
    StateName,
    CountryID
FROM 
	State
WHERE 
	CountryID BETWEEN 1 AND 3;

-- Find Cities Where CityName Starts with ‘A’
SELECT 
	CityID,
    CityName,
    CityCode
FROM 
	City
WHERE 
	CityName LIKE 'A%';

-- Find Students Who Belong to Specific CountryIDs
SELECT 
	StudentID,
    FirstName,
    LastName,
    CountryID
FROM 
	Student
WHERE 
	CountryID IN (1, 3, 5);

-- Find Students with Non-Null GPA Values
SELECT 
	StudentID,
    FirstName,
    LastName,
    GPA
FROM 
	Student
WHERE 
	GPA IS NOT NULL;