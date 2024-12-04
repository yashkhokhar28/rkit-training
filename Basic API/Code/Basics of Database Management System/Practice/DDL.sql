-- CREATE

-- Create Table
CREATE TABLE Books (
    BookID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Author VARCHAR(50),
    Genre VARCHAR(50),
    Price DECIMAL(10, 2),
    Stock INT
);
-- Create Database
CREATE DATABASE LibraryDB;

-- Create Index
CREATE INDEX idx_genre ON Books (Genre);

-- Create View 
CREATE VIEW BorrowedBooks AS
SELECT 
	b.Title, m.Name AS Borrower, br.BorrowDate
FROM 
	Borrow br
JOIN 
	Books b ON br.BookID = b.BookID
JOIN 
	Members m ON br.MemberID = m.MemberID;

-- ALTER

-- Add a Column
ALTER TABLE Books ADD Publisher VARCHAR(50);

-- Modify a Column
ALTER TABLE Books MODIFY Price DECIMAL(12, 2);

-- Rename a Column
ALTER TABLE Books CHANGE Publisher PublisherName VARCHAR(100);

-- Drop a Column
ALTER TABLE Books DROP COLUMN PublisherName;

-- Add a Constraint
ALTER TABLE Borrow ADD CONSTRAINT fk_member FOREIGN KEY (MemberID) REFERENCES Members(MemberID);

-- Rename a Table
ALTER TABLE Members RENAME TO LibraryMembers;

-- DROP

-- Drop a Table
DROP TABLE Borrow;

-- Drop a Database
DROP DATABASE LibraryDB;

-- Drop an Index
DROP INDEX idx_genre ON Books;

-- Drop a View
DROP VIEW BorrowedBooks;

-- TRUNCATE-- 

-- Truncate a Table
TRUNCATE TABLE Members;