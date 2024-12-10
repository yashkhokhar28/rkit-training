-- Creating an Index on CountryName in the Country Table:
CREATE INDEX 
	idx_country_name
ON 
	Country (CountryName);

-- Creating a Composite Index on StateName and CityName in the State and City Tables:
CREATE INDEX 
	idx_state_city
ON 
	State (StateName, StateCode);
    
-- Unique Index on PhoneNumber Column in the Student Table:
CREATE UNIQUE INDEX 
	idx_student_phone
ON 
	Student (PhoneNumber);