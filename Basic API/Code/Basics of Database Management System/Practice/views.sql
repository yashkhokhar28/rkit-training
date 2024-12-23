-- Creating a View for Active Students
CREATE VIEW vws_ActiveStudents AS
SELECT 
	StudentID,
	FirstName,
    LastName
FROM 
	Student
WHERE 
	Status = 'Active';
    
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
    
-- Creating a View with Joins
CREATE VIEW vws_StudentCourses AS
SELECT 
	S.StudentID,
    S.FirstName,
    S.LastName,
    C.CourseName
FROM 
	Student S
JOIN 
	Enrollment E ON S.StudentID = E.StudentID
JOIN 
	Courses C ON E.CourseID = C.CourseID;
    
-- Updatable Views
UPDATE 
	vws_ActiveStudents
SET 
	Status = 'Inactive'
WHERE 
	StudentID = 123;