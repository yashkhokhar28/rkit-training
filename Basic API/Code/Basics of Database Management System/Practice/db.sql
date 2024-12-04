CREATE DATABASE library_db;
USE library_db;

-- Books table
CREATE TABLE Books (
    BookID INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(100) NOT NULL,
    Author VARCHAR(50) NOT NULL,
    Genre VARCHAR(30),
    Price DECIMAL(10, 2),
    Stock INT DEFAULT 0
);

-- Members table
CREATE TABLE Members (
    MemberID INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    JoinDate DATE
);

-- Borrow table
CREATE TABLE Borrow (
    BorrowID INT AUTO_INCREMENT PRIMARY KEY,
    BookID INT,
    MemberID INT,
    BorrowDate DATE,
    ReturnDate DATE,
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (MemberID) REFERENCES Members(MemberID)
);

INSERT INTO Books (Title, Author, Genre, Price, Stock) VALUES
('The Great Gatsby', 'F. Scott Fitzgerald', 'Classic', 299.99, 5),
('1984', 'George Orwell', 'Dystopian', 199.99, 3),
('To Kill a Mockingbird', 'Harper Lee', 'Classic', 249.99, 7),
('The Catcher in the Rye', 'J.D. Salinger', 'Classic', 199.99, 4),
('Brave New World', 'Aldous Huxley', 'Science Fiction', 299.99, 6);


INSERT INTO Members (Name, Email, JoinDate) VALUES
('John Doe', 'john.doe@example.com', '2024-11-01'),
('Jane Smith', 'jane.smith@example.com', '2024-11-15'),
('Alice Johnson', 'alice.johnson@example.com', '2024-12-01'),
('Bob Brown', 'bob.brown@example.com', '2024-12-02'),
('Charlie Davis', 'charlie.davis@example.com', '2024-12-03');

INSERT INTO Borrow (BookID, MemberID, BorrowDate, ReturnDate) VALUES
(1, 1, '2024-12-01', NULL), 
(2, 2, '2024-12-01', '2024-12-10'), 
(3, 3, '2024-12-02', NULL), 
(4, 4, '2024-12-03', '2024-12-05'),
(5, 5, '2024-12-04', NULL);