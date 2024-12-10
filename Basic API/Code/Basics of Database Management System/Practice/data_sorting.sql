-- Sort Students by GPA in Descending Order
SELECT 
	FirstName,
    GPA
FROM 
	Student
Order By
	GPA DESC;
    
-- Sort Cities Alphabetically
SELECT 
	CityName 
FROM 
	City 
Order By 
	CityName;
    
-- Sort States by CountryID and then by StateName
SELECT 
	CountryID,
	StateName
FROM 
	State 
Order By 
	CountryID,StateName;   