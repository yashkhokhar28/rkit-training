-- Creating a View for Active Students
DROP VIEW vws_ActiveStudents;
CREATE VIEW vws_ActiveStudents AS
SELECT 
	StudentID,
	FirstName,
    LastName,
    IsFullTime
FROM 
	Student
WHERE 
	IsFullTime = 1;
SELECT * FROM student;
select * from vws_ActiveStudents;
DELETE FROM vws_ActiveStudents WHERE StudentID = 2;
    
-- Joining a View with Another Table
SELECT 
	ActS.StudentID,
    ActS.FirstName,
    C.CourseName
FROM 
	vws_ActiveStudents AS ActS
JOIN 
	Enrollment E ON ActS.StudentID = E.StudentID
JOIN 
	Courses C ON E.CourseID = C.CourseID;
    
-- Updatable Views
UPDATE 
	vws_ActiveStudents
SET 
	IsFullTime = 0
WHERE 
	StudentID = 1;