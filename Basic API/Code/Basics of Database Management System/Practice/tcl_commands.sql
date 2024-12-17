-- COMMIT Statement
BEGIN;
UPDATE Student SET GPA = 9.0 WHERE StudentID = 1;
INSERT INTO Student (FirstName, Gender, BirthDate) VALUES ('Emma', 'Female', '2002-04-15');
COMMIT;

-- ROLLBACK Statement
BEGIN;
UPDATE Student SET GPA = 8.0 WHERE StudentID = 1;
DELETE FROM Student WHERE StudentID = 10;

-- An error occurs; roll back all changes
ROLLBACK;

-- SAVEPOINT Statement
BEGIN;

UPDATE Student SET GPA = 9.5 WHERE StudentID = 2;
SAVEPOINT SavePoint1;

UPDATE Student SET GPA = 7.5 WHERE StudentID = 3;
ROLLBACK TO SavePoint1;

COMMIT;