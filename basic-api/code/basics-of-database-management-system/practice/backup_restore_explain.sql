-- Backup

-- Full Database Backup (using mysqldump)
mysqldump -u username -p database_name > backup.sql;

-- Backup specific tables
mysqldump -u username -p database_name table_name > table_backup.sql;

-- Restore
-- Restore full database from backup
mysql -u username -p database_name < backup.sql;

-- Restore specific table from backup
mysql -u username -p database_name < table_backup.sql;

-- EXPLAIN Keyword
EXPLAIN 
SELECT 
	s.StudentID,
	s.FirstName,
    c.CityName,
    st.StateName
FROM 
	Student s
JOIN 
	City c ON s.CityID = c.CityID
JOIN 
	State st ON s.StateID = st.StateID
WHERE 
	st.StateName = 'Gujarat';