-- Create database
CREATE DATABASE ecommerce_db;
USE ecommerce_db;

-- Create User table
CREATE TABLE User (
    UserID INT AUTO_INCREMENT PRIMARY KEY,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    Password VARCHAR(100) NOT NULL,
    PhoneNumber VARCHAR(15),
    Address TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Category table
CREATE TABLE Category (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(50) UNIQUE NOT NULL,
    Description TEXT,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

-- Create Product table
CREATE TABLE Product (
    ProductID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryID INT,
    ProductName VARCHAR(100) UNIQUE NOT NULL,
    ProductDescription TEXT,
    Price DECIMAL(10, 2) NOT NULL,
    StockQuantity INT DEFAULT 0,
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- Create Cart table
CREATE TABLE Cart (
    CartID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    ProductID INT,
    Quantity INT DEFAULT 1,
    AddedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserID) REFERENCES User(UserID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Create Address table
CREATE TABLE Address (
    AddressID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    Street VARCHAR(255),
    City VARCHAR(100),
    State VARCHAR(100),
    ZipCode VARCHAR(20),
    Country VARCHAR(50),
    FOREIGN KEY (UserID) REFERENCES User(UserID)
);

-- Create Order table
CREATE TABLE `Order` (
    OrderID INT AUTO_INCREMENT PRIMARY KEY,
    UserID INT,
    OrderDate TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    TotalAmount DECIMAL(10, 2) NOT NULL,
    OrderStatus ENUM('Pending', 'Shipped', 'Delivered', 'Cancelled') DEFAULT 'Pending',
    ShippingAddressID INT,
    FOREIGN KEY (UserID) REFERENCES User(UserID),
    FOREIGN KEY (ShippingAddressID) REFERENCES Address(AddressID)
);

-- Create OrderDetails table
CREATE TABLE OrderDetails (
    OrderDetailID INT AUTO_INCREMENT PRIMARY KEY,
    OrderID INT,
    ProductID INT,
    Quantity INT,
    Price DECIMAL(10, 2),
    FOREIGN KEY (OrderID) REFERENCES `Order`(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Data Sorting (ORDER BY)
SELECT * FROM Product ORDER BY Price ASC;
SELECT * FROM User ORDER BY LastName DESC, FirstName DESC;

-- Null Value & Keyword
SELECT * FROM User WHERE PhoneNumber IS NULL;
SELECT * FROM Product WHERE StockQuantity IS NULL;

-- Insert Data (DML)
INSERT INTO Product (CategoryID, ProductName, ProductDescription, Price, StockQuantity) 
VALUES (1, 'Smartphone', 'Latest model smartphone', 499.99, 50);

-- Update Data (DML)
UPDATE Product SET Price = 450.00 WHERE ProductID = 1;

-- DDL Example (Data Definition Language)
CREATE TABLE NewCategory (
    CategoryID INT PRIMARY KEY,
    CategoryName VARCHAR(50)
);
DROP TABLE OrderDetails;

-- DCL Example (Data Control Language)
GRANT SELECT ON User TO 'role_name';
REVOKE SELECT ON Product FROM 'role_name';

-- TCL Example (Transaction Control Language)
START TRANSACTION;
INSERT INTO `Order` (UserID, TotalAmount) VALUES (1, 1000.00);
UPDATE Product SET StockQuantity = StockQuantity - 1 WHERE ProductID = 1;
COMMIT;
ROLLBACK;

-- DQL Example (Data Query Language)
SELECT * FROM Product WHERE StockQuantity > 0;
SELECT * FROM `Order` WHERE UserID = 1;

-- Limit Example
SELECT * FROM Product ORDER BY Price LIMIT 5;
SELECT * FROM User LIMIT 10;

-- Aggregate Functions
SELECT COUNT(*) AS TotalProducts FROM Product;
SELECT AVG(Price) AS AveragePrice FROM Product;

-- Sub-Queries
SELECT * FROM User WHERE UserID IN (SELECT UserID FROM `Order` WHERE TotalAmount > 500);
SELECT * FROM Product WHERE CategoryID IN (SELECT CategoryID FROM Product GROUP BY CategoryID HAVING COUNT(*) >= 10);

-- Joins
SELECT o.OrderID, od.ProductID, od.Quantity
FROM `Order` o
INNER JOIN OrderDetails od ON o.OrderID = od.OrderID;

SELECT u.UserID, u.FirstName, u.LastName, o.OrderID
FROM User u
LEFT JOIN `Order` o ON u.UserID = o.UserID;

-- Unions
SELECT ProductName AS Name FROM Product
UNION
SELECT FirstName AS Name FROM User;

SELECT ProductID FROM Product
UNION
SELECT OrderID FROM `Order`;

-- Index Example
CREATE INDEX idx_email ON User(Email);
CREATE INDEX idx_product_category_price ON Product(CategoryID, Price);

-- View Example
CREATE VIEW ProductWithCategory AS
SELECT p.ProductID, p.ProductName, c.CategoryName
FROM Product p
INNER JOIN Category c ON p.CategoryID = c.CategoryID;

SELECT * FROM ProductWithCategory;

-- Backup & Restore, Explain Keyword
mysqldump -u root -p ecommerce_db > ecommerce_db_backup.sql
mysql -u root -p ecommerce_db < ecommerce_db_backup.sql
EXPLAIN SELECT * FROM Product WHERE Price > 100;