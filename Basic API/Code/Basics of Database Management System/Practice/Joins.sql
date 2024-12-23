-- INNER JOIN
-- Find all students along with their city and state names.
SELECT 
	Student.FirstName,
	Student.LastName,
	City.CityName,
    State.StateName
FROM 
	Student
INNER JOIN 
	City ON Student.CityID = City.CityID
INNER JOIN 
	State ON Student.StateID = State.StateID;
    
-- LEFT JOIN (or LEFT OUTER JOIN)
-- Find all students, along with their city and state names. Include students even if they don’t have a city or state associated.
SELECT 
	Student.FirstName,
    Student.LastName,
    City.CityName,
    State.StateName
FROM 
	Student
LEFT JOIN 
	City ON Student.CityID = City.CityID
LEFT JOIN 
	State ON Student.StateID = State.StateID;
    
-- RIGHT JOIN (or RIGHT OUTER JOIN)
-- Find all cities, along with the students and their state names. Include cities even if no student is associated with them.
SELECT 
	City.CityName,
	Student.FirstName,
    Student.LastName,
    State.StateName
FROM 
	City
RIGHT JOIN 
	Student ON City.CityID = Student.CityID
RIGHT JOIN 
	State ON Student.StateID = State.StateID;
    
-- FULL JOIN (or FULL OUTER JOIN)
SELECT 
	Student.FirstName,
    Student.LastName,
    City.CityName,
    State.StateName
FROM 
	Student
LEFT JOIN 
	City ON Student.CityID = City.CityID
LEFT JOIN 
	State ON Student.StateID = State.StateID;
UNION
SELECT 
	City.CityName,
	Student.FirstName,
    Student.LastName,
    State.StateName
FROM 
	City
RIGHT JOIN 
	Student ON City.CityID = Student.CityID
RIGHT JOIN 
	State ON Student.StateID = State.StateID;

-- SELF JOIN
-- Find pairs of students who live in the same city.
SELECT 
	A.FirstName AS Student1_FirstName,
	A.LastName AS Student1_LastName, 
	B.FirstName AS Student2_FirstName,
    B.LastName AS Student2_LastName, 
	City.CityName
FROM 
	Student A
INNER JOIN 
	Student B ON A.CityID = B.CityID AND A.StudentID != B.StudentID
INNER JOIN 
	City ON A.CityID = City.CityID;
    
-- JOIN with Multiple Tables
-- Find all students along with their country, state, and city names.
SELECT 
	Student.FirstName,
    Student.LastName,
    Country.CountryName,
    State.StateName,
    City.CityName
FROM 
	Student
JOIN 
	Country ON Student.CountryID = Country.CountryID
JOIN 
	State ON Student.StateID = State.StateID
JOIN 
	City ON Student.CityID = City.CityID;