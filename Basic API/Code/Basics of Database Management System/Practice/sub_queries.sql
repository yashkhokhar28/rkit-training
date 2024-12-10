-- Subquery in the WHERE Clause
-- Find all students whose GPA is higher than the average GPA in their respective country.
SELECT 
	FirstName,
	LastName,
    GPA,
    Country.CountryName
FROM 
	Student
JOIN 
	Country ON Student.CountryID = Country.CountryID
WHERE 
	GPA > (
    SELECT 
		AVG(GPA) 
	FROM 
		Student 
    WHERE 
		CountryID = Student.CountryID
);

-- Subquery in the SELECT Clause
-- Find each student’s name and the number of cities they have lived in.
SELECT 
	FirstName,
	LastName,
       (SELECT 
			COUNT(*) 
        FROM 
			City 
        WHERE 
			CityID = Student.CityID) AS CityCount
FROM Student;

-- Subquery in the FROM Clause
-- Find all students who are enrolled in the states with the highest number of students.
SELECT 
	FirstName,
    LastName,
    StateName
FROM 
	Student
JOIN 
	State ON Student.StateID = State.StateID
WHERE 
	State.StateID IN (
		SELECT 
			StateID 
		FROM 
			Student 
		GROUP BY 
			StateID 
		HAVING 
			COUNT(StudentID) = (
		SELECT 
			MAX(StudentCount) 
        FROM 
			(SELECT 
				COUNT(StudentID) AS StudentCount
			FROM 
				Student 
			GROUP BY StateID) AS StateCounts
    )
);

-- Subquery with EXISTS
-- Find all students who live in a city where there are students from multiple countries.
SELECT 
	FirstName,
	LastName,
    CityName
FROM 
	Student
JOIN 
	City ON Student.CityID = City.CityID
WHERE 
	EXISTS (
		SELECT 1 
		FROM 
			Student AS s2 
		WHERE 
			s2.CityID = Student.CityID 
		AND 
			s2.CountryID != Student.CountryID
);

-- Subquery with IN

-- Find students who live in cities located in the countries that are part of the European Union (assuming European countries have IDs in the Country table).
SELECT 
	FirstName,
	LastName,
    CityName
FROM 
	Student
JOIN 
	City ON Student.CityID = City.CityID
WHERE 
	Student.CountryID IN (
		SELECT 
			CountryID 
		FROM 
			Country
		WHERE 
		CountryName IN ('Germany', 'France', 'Spain', 'Italy', 'Netherlands')
);