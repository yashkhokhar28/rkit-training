-- SELECT 

-- Select All Columns
SELECT 
	* 
FROM 
	Student;

-- Select Specific Columns
SELECT 
	StudentID,
    FirstName,
    LastName
FROM 
	Student;

-- Select With Conditions
SELECT 
	FirstName,
    LastName 
FROM 
	Student 
WHERE 
	IsFullTime > TRUE;

-- Select With Sorting
SELECT 
	FirstName,
    LastName 
FROM 
	Student 
ORDER BY 
	GPA DESC;

-- Select With Aggregation
SELECT 
	City,
    AVG(GPA) AS AvgGPA 
FROM 
	Student 
GROUP BY 
	City;

-- Select With Join
SELECT 
	stu.StudentID,
    stu.FirstName,
    c.CityName
FROM 
	Student stu
JOIN 
	City c ON stu.CityID = c.CityID;

-- Select With Subquery
SELECT 
	FirstName 
FROM 
	Student 
WHERE 
	CityID > (SELECT
				CityID
			FROM 
				City 
			Where 
				CityCode = 'AU');