CREATE DATABASE LINQ;
USE LINQ;

CREATE TABLE Product (
    ProductID INT AUTO_INCREMENT PRIMARY KEY,
    ProductName VARCHAR(50) NOT NULL,
    ProductDescription VARCHAR(50) NOT NULL,
    ProductCode VARCHAR(7) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Laptop', 'High-performance laptop', 'PRD001');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Smartphone', 'Latest model smartphone', 'PRD002');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Headphones', 'Noise-cancelling headphones', 'PRD003');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Monitor', '4K resolution monitor', 'PRD004');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Keyboard', 'Mechanical keyboard', 'PRD005');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Mouse', 'Ergonomic wireless mouse', 'PRD006');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Tablet', '10-inch display tablet', 'PRD007');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Smartwatch', 'Fitness tracking smartwatch', 'PRD008');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Printer', 'All-in-one printer', 'PRD009');

INSERT INTO Product (ProductName, ProductDescription, ProductCode) 
VALUES ('Router', 'Dual-band Wi-Fi router', 'PRD010');

-- Create the Category table with a foreign key reference to ProductID
CREATE TABLE Category (
    CategoryID INT AUTO_INCREMENT PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL,
    ProductID INT,
    FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);

-- Insert data into the Category table
INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Electronics', 1);  -- Laptop

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Mobile Devices', 2);  -- Smartphone

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Audio', 3);  -- Headphones

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Electronics', 4);  -- Monitor

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Peripherals', 5);  -- Keyboard

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Peripherals', 6);  -- Mouse

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Mobile Devices', 7);  -- Tablet

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Wearables', 8);  -- Smartwatch

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Office Equipment', 9);  -- Printer

INSERT INTO Category (CategoryName, ProductID) 
VALUES ('Networking', 10);  -- Router



