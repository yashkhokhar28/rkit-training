-- Create User Table as usr01
CREATE TABLE usr01 (
    r01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: UserID',
    r01f02 VARCHAR(50) NOT NULL COMMENT 'Original Column: FirstName',
    r01f03 VARCHAR(50) NOT NULL COMMENT 'Original Column: LastName',
    r01f04 VARCHAR(100) UNIQUE NOT NULL COMMENT 'Original Column: Email',
    r01f05 VARCHAR(100) NOT NULL COMMENT 'Original Column: Password',
    r01f06 VARCHAR(15) COMMENT 'Original Column: PhoneNumber',
    r01f07 TEXT COMMENT 'Original Column: Address',
    r01f08 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: CreatedAt',
    r01f09 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: UpdatedAt'
) COMMENT = 'Original Table: User';

-- Create Category Table as cat01
CREATE TABLE cat01 (
    t01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: CategoryID',
    t01f02 VARCHAR(50) UNIQUE NOT NULL COMMENT 'Original Column: CategoryName',
    t01f03 TEXT COMMENT 'Original Column: Description',
    t01f04 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: CreatedAt',
    t01f05 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: UpdatedAt'
) COMMENT = 'Original Table: Category';

-- Create Product Table as pro01
CREATE TABLE pro01 (
    o01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: ProductID',
    o01f02 INT COMMENT 'Original Column: CategoryID',
    o01f03 VARCHAR(100) UNIQUE NOT NULL COMMENT 'Original Column: ProductName',
    o01f04 TEXT COMMENT 'Original Column: ProductDescription',
    o01f05 DECIMAL(10, 2) NOT NULL COMMENT 'Original Column: Price',
    o01f06 INT DEFAULT 0 COMMENT 'Original Column: StockQuantity',
    o01f07 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: CreatedAt',
    o01f08 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: UpdatedAt'
) COMMENT = 'Original Table: Product';

-- Create Cart Table as car01
CREATE TABLE car01 (
    r01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: CartID',
    r01f02 INT COMMENT 'Original Column: UserID',
    r01f03 INT COMMENT 'Original Column: ProductID',
    r01f04 INT DEFAULT 1 COMMENT 'Original Column: Quantity',
    r01f05 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: AddedAt'
) COMMENT = 'Original Table: Cart';

-- Create Address Table as add01
CREATE TABLE add01 (
    d01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: AddressID',
    d01f02 INT COMMENT 'Original Column: UserID',
    d01f03 VARCHAR(255) COMMENT 'Original Column: Street',
    d01f04 VARCHAR(100) COMMENT 'Original Column: City',
    d01f05 VARCHAR(100) COMMENT 'Original Column: State',
    d01f06 VARCHAR(20) COMMENT 'Original Column: ZipCode',
    d01f07 VARCHAR(50) COMMENT 'Original Column: Country'
) COMMENT = 'Original Table: Address';

-- Create Order Table as ord01
CREATE TABLE ord01 (
    d01f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: OrderID',
    d01f02 INT COMMENT 'Original Column: UserID',
    d01f03 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'Original Column: OrderDate',
    d01f04 DECIMAL(10, 2) NOT NULL COMMENT 'Original Column: TotalAmount',
    d01f05 ENUM('P', 'S', 'D', 'C') DEFAULT 'P' COMMENT 'Original Column: OrderStatus',
    d01f06 INT COMMENT 'Original Column: ShippingAddressID'
) COMMENT = 'Original Table: Order';

-- Create OrderDetails Table as ord02
CREATE TABLE ord01det (
    d02f01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'Original Column: OrderDetailID',
    d02f02 INT COMMENT 'Original Column: OrderID',
    d02f03 INT COMMENT 'Original Column: ProductID',
    d02f04 INT COMMENT 'Original Column: Quantity',
    d02f05 DECIMAL(10, 2) COMMENT 'Original Column: Price'
) COMMENT = 'Original Table: OrderDetails';

USE ecommerce_db;

-- Insert data into usr01 (User Table)
INSERT INTO usr01 (r01f02, r01f03, r01f04, r01f05, r01f06, r01f07)
VALUES
('John', 'Doe', 'john.doe@example.com', 'Password123', '9876543210', '123 Main St, City A'),
('Jane', 'Smith', 'jane.smith@example.com', 'SecurePass456', '8765432109', '456 Elm St, City B'),
('Alice', 'Johnson', 'alice.johnson@example.com', 'Alice789!', '7654321098', '789 Oak St, City C'),
('Bob', 'Brown', 'bob.brown@example.com', 'Bob!2024', '6543210987', '321 Pine St, City D'),
('Charlie', 'Taylor', 'charlie.taylor@example.com', 'Charli3!', '5432109876', '654 Cedar St, City E');

-- Insert data into cat01 (Category Table)
INSERT INTO cat01 (t01f02, t01f03)
VALUES
('Electronics', 'Devices and gadgets like smartphones and laptops'),
('Clothing', 'Apparel including shirts, trousers, and jackets'),
('Home & Kitchen', 'Furniture, utensils, and home appliances'),
('Books', 'Fictional and non-fictional books across genres'),
('Toys', 'Toys and games for children and adults');

-- Insert data into pro01 (Product Table)
INSERT INTO pro01 (o01f02, o01f03, o01f04, o01f05, o01f06)
VALUES
(1, 'Smartphone XYZ', 'Latest model with advanced features', 699.99, 50),
(1, 'Laptop ABC', 'High-performance laptop for professionals', 1299.99, 30),
(2, 'Men\'s Jacket', 'Stylish winter jacket for men', 79.99, 100),
(3, 'Microwave Oven', 'Compact and efficient microwave', 199.99, 40),
(4, 'Fantasy Novel', 'Bestselling fantasy book series', 15.99, 200);

-- Insert data into car01 (Cart Table)
INSERT INTO car01 (r01f02, r01f03, r01f04)
VALUES
(1, 1, 2),
(1, 2, 1),
(2, 3, 1),
(3, 4, 3),
(4, 5, 1);

-- Insert data into add01 (Address Table)
INSERT INTO add01 (d01f02, d01f03, d01f04, d01f05, d01f06, d01f07)
VALUES
(1, '123 Main St', 'City A', 'State A', '12345', 'Country A'),
(2, '456 Elm St', 'City B', 'State B', '54321', 'Country B'),
(3, '789 Oak St', 'City C', 'State C', '67890', 'Country C'),
(4, '321 Pine St', 'City D', 'State D', '98765', 'Country D'),
(5, '654 Cedar St', 'City E', 'State E', '56789', 'Country E');

-- Insert data into ord01 (Order Table)
INSERT INTO ord01 (d01f02, d01f04, d01f05, d01f06)
VALUES
(1, 999.97, 'P', 1),
(2, 159.98, 'S', 2),
(3, 599.97, 'D', 3),
(4, 15.99, 'C', 4),
(5, 249.98, 'P', 5);

-- Insert data into ord01det (OrderDetails Table)
INSERT INTO ord01det (d02f02, d02f03, d02f04, d02f05)
VALUES
(1, 1, 1, 699.99),
(1, 2, 1, 1299.99),
(2, 3, 2, 79.99),
(3, 4, 3, 199.99),
(4, 5, 1, 15.99);

-- Data Sorting (ORDER BY)
-- Sort products by price (o01f05 = Price)
SELECT 
	ProductName
FROM
	pro01
ORDER BY 
	o01f05 ASC;

-- Sort users by last name and first name (r01f03 = LastName, r01f02 = FirstName)
SELECT 
	ProductName 
FROM 
	usr01 
ORDER BY 
	r01f03 DESC,
    r01f02 DESC;

-- Null Value & Keyword
-- Fetch users without a phone number (r01f06 = PhoneNumber)
SELECT 
	ProductName 
FROM 
	usr01 
WHERE r01f06 IS NULL;

-- Fetch products where stock quantity is null (o01f06 = StockQuantity)
SELECT 
	ProductName 
FROM 
	pro01 
WHERE o01f06 IS NULL;

-- Insert Data (DML)
-- Insert new product with category ID, name, description, price, and stock quantity
INSERT INTO pro01 (o01f02, o01f03, o01f04, o01f05, o01f06) 
VALUES (1, 'Smartphone', 'Latest model smartphone', 499.99, 50);

-- Update Data (DML)
-- Update product price (o01f05 = Price) where product ID matches (o01f01 = ProductID)
UPDATE 
	pro01 
SET o01f05 = 450.00 
WHERE o01f01 = 1;

-- DDL Example (Data Definition Language)
-- Create a new table with simplified structure
CREATE TABLE newcat02 (
    newcat02f01 INT PRIMARY KEY,
    newcat02f02 VARCHAR(50)
);

-- Drop the order details table
DROP TABLE ord01det;

-- DCL Example (Data Control Language)
-- Grant SELECT permission on the User table (usr01) to 'role_name'
GRANT SELECT ON usr01 TO 'role_name';

-- Revoke SELECT permission on the Product table (pro01) from 'role_name'
REVOKE SELECT ON pro01 FROM 'role_name';

-- TCL Example (Transaction Control Language)
-- Insert an order (d01f02 = UserID, d01f04 = TotalAmount)
START TRANSACTION;
INSERT INTO ord01 (d01f02, d01f04) VALUES (1, 1000.00);

-- Update stock quantity for a product (o01f06 = StockQuantity, o01f01 = ProductID)
UPDATE pro01 SET o01f06 = o01f06 - 1 WHERE o01f01 = 1;
COMMIT;
ROLLBACK;

-- DQL Example (Data Query Language)
-- Fetch products with stock quantity greater than zero (o01f06 = StockQuantity)
SELECT 
	ProductName
FROM 
	pro01 
WHERE o01f06 > 0;

-- Fetch orders for a specific user (d01f02 = UserID)
SELECT 
	OrderID 
FROM 
	ord01 
WHERE d01f02 = 1;

-- Limit Example
-- Fetch the 5 cheapest products (o01f05 = Price)
SELECT 
	ProductName 
FROM 
	pro01 
ORDER BY o01f05 LIMIT 5;

-- Fetch the first 10 users
SELECT 
	UserID 
FROM usr01 LIMIT 10;

-- Aggregate Functions
-- Count total products
SELECT 
	COUNT(ProductName) AS TotalProducts 
FROM 
	pro01;

-- Calculate the average price of products (o01f05 = Price)
SELECT 
	AVG(o01f05) AS AveragePrice 
FROM 
	pro01;

-- Sub-Queries
-- Fetch users who placed orders with a total amount greater than 500 (d01f04 = TotalAmount)
SELECT 
	UserID 
FROM 
	usr01 
WHERE 
	r01f01 IN (
		SELECT 
			d01f02 
		FROM 
			ord01 
		WHERE d01f04 > 500
);

-- Fetch products belonging to categories with 10 or more products (o01f02 = CategoryID)
SELECT 
	ProductName 
FROM 
	pro01 
WHERE 
	o01f02 IN (
			SELECT 
				t01f01 
			FROM 
				cat01 
			GROUP BY 
				t01f01 
            HAVING 
				COUNT(*) >= 10
);

-- Joins
-- Fetch order details: order ID, product ID, and quantity (t01f02 = OrderID, t01f03 = ProductID, t01f04 = Quantity)
SELECT 
	o.d01f01,
    od.t01f03,
    od.t01f04
FROM 
	ord01 o
INNER JOIN 
	ord01det od 
ON o.d01f01 = od.t01f02;

-- Fetch users and their orders: user ID, first name, last name, and order ID
SELECT 
	u.r01f01,
    u.r01f02,
    u.r01f03,
    o.d01f01
FROM 
	usr01 u
LEFT JOIN 
	ord01 o 
ON u.r01f01 = o.d01f02;

-- Unions
-- Fetch a union of product names (o01f03 = ProductName) and user first names (r01f02 = FirstName)
SELECT 
	o01f03 AS Name 
FROM 
	pro01
UNION
SELECT 
	r01f02 AS Name 
FROM 
	usr01;

-- Fetch a union of product IDs (o01f01 = ProductID) and order IDs (d01f01 = OrderID)
SELECT 
	o01f01 
FROM 
	pro01
UNION
SELECT 
	d01f01 
FROM 
	ord01;

-- Index Example
-- Create an index on user emails (r01f04 = Email)
CREATE INDEX idx_r01f04 ON usr01(r01f04);

-- Create a composite index on product category and price (o01f02 = CategoryID, o01f05 = Price)
CREATE INDEX idx_o01f02_o01f05 ON pro01(o01f02, o01f05);

-- View Example
-- Create a view for products with their category names
CREATE VIEW vws_ProductWithCategory AS
SELECT p.o01f01, p.o01f03, c.t01f02
FROM pro01 p
INNER JOIN cat01 c ON p.o01f02 = c.t01f01;

-- Query the view
SELECT * FROM vws_ProductWithCategory;