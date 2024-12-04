-- INSERT

-- Insert Single Row
INSERT INTO Books (Title, Author, Genre, Price, Stock) 
VALUES ('The Alchemist', 'Paulo Coelho', 'Fiction', 349.99, 10);

-- Insert Multiple Rows
INSERT INTO Members (Name, Email, JoinDate) 
VALUES 
('Emily Brown', 'emily.brown@example.com', '2024-12-01'),
('Liam White', 'liam.white@example.com', '2024-12-05');

-- Insert Data From Another Table
INSERT INTO Borrow (BookID, MemberID, BorrowDate)
SELECT BookID, MemberID, '2024-12-05' FROM Members WHERE MemberID = 2;

-- UPDATE
-- Update Single Column
UPDATE Books
SET Price = 299.99
WHERE Title = 'The Alchemist';

-- Update Multiple Columns
UPDATE Members
SET Email = 'john.doe2024@example.com', JoinDate = '2024-12-10'
WHERE Name = 'John Doe';

-- Update With a Condition
UPDATE Books
SET Stock = Stock - 1
WHERE BookID = 1 AND Stock > 0;


-- DELETE

-- Delete Specific Rows
DELETE FROM Members 
WHERE Name = 'Liam White';

-- Delete All Rows From a Table
DELETE FROM Borrow;

-- Delete With a Condition
DELETE FROM Books 
WHERE Stock = 0;