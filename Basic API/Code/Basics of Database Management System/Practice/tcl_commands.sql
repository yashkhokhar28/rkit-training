START TRANSACTION;

INSERT INTO Student (FirstName, Gender, BirthDate) VALUES ('Emma', 'Female', '2002-04-15');

-- Set a savepoint before another update
SAVEPOINT before_update;

-- Update a student's grade
UPDATE Student SET Gender = 'Male' WHERE FirstName = 'Emma';

-- If an issue occurs, rollback to the savepoint
ROLLBACK TO SAVEPOINT before_update;

-- Finally, commit the transaction if all is good
COMMIT;
