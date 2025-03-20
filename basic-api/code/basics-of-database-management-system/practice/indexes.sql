-- Creating an Index on CountryName in the Country Table:
CREATE INDEX 
	idx_CountryName
ON 
	Country (CountryName);

-- Creating a Composite Index on StateName and CityName in the State and City Tables:
CREATE INDEX 
	idx_StateName_StateCode
ON 
	State (StateName, StateCode);
    