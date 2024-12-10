-- Basic Example: Limit the number of rows returned.
SELECT 
	* 
FROM 
	Student
LIMIT 5;

-- Limiting Results with ORDER BY: Limiting the number of rows after sorting.
SELECT 
	* 
FROM 
	Student
ORDER BY 
	GPA DESC
LIMIT 3;

-- Paginated Results: Fetch rows in chunks, useful for pagination.
SELECT 
	* 
FROM 
	Student
LIMIT 10 OFFSET 20;

-- Top N Results: Limiting results to get the top N records.
SELECT 
	*
FROM 
	City
ORDER BY 
	Population DESC
LIMIT 5;

-- Limit with Aggregate Functions: Limiting results after aggregation.
SELECT 
	CountryID,
    AVG(GPA) as AverageGPA
FROM 
	Student
GROUP BY 
	CountryID
ORDER BY 
	AverageGPA DESC
LIMIT 3;