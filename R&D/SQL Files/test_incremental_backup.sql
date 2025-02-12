INSERT INTO orders_1 (
    `index`, Order_ID, Date, Status, Fulfilment, Sales_Channel, 
    Ship_Service_Level, Style, SKU, Category, Size, ASIN, Courier_Status, 
    Qty, Currency, Amount, Ship_City, Ship_State, Ship_Postal_Code, 
    Ship_Country, Promotion_IDs, B2B, Fulfilled_By, created_at
) VALUES
(1, 'ORD001', CURDATE(), 'Pending', 'Amazon FBA', 'Online Store', 'Standard', 'Casual', 'SKU123', 'Clothing', 'M', 'B000123', 'Shipped', 2, 'USD', 49.99, 'New York', 'NY', '10001', 'USA', NULL, 'No', 'Amazon', NOW()),
(2, 'ORD002', CURDATE(), 'Shipped', 'Amazon FBM', 'Retail', 'Express', 'Formal', 'SKU124', 'Clothing', 'L', 'B000124', 'Delivered', 1, 'USD', 79.99, 'Los Angeles', 'CA', '90001', 'USA', NULL, 'Yes', 'Third-Party', NOW()),
(3, 'ORD003', CURDATE(), 'Processing', 'Amazon FBA', 'Marketplace', 'Standard', 'Casual', 'SKU125', 'Footwear', '10', 'B000125', 'In Transit', 3, 'USD', 129.99, 'Chicago', 'IL', '60601', 'USA', NULL, 'No', 'Amazon', NOW()),
(4, 'ORD004', CURDATE(), 'Delivered', 'Amazon FBM', 'Wholesale', 'Overnight', 'Sports', 'SKU126', 'Equipment', 'XL', 'B000126', 'Delivered', 5, 'USD', 199.99, 'Houston', 'TX', '77001', 'USA', NULL, 'Yes', 'Third-Party', NOW()),
(5, 'ORD005', CURDATE(), 'Canceled', 'Amazon FBA', 'Online Store', 'Standard', 'Casual', 'SKU127', 'Clothing', 'S', 'B000127', 'Returned', 2, 'USD', 59.99, 'Miami', 'FL', '33101', 'USA', NULL, 'No', 'Amazon', NOW()),
(6, 'ORD006', CURDATE(), 'Shipped', 'Amazon FBA', 'Retail', 'Express', 'Outdoor', 'SKU128', 'Camping', 'M', 'B000128', 'Out for Delivery', 1, 'USD', 89.99, 'Seattle', 'WA', '98101', 'USA', NULL, 'No', 'Amazon', NOW()),
(7, 'ORD007', CURDATE(), 'Processing', 'Amazon FBM', 'Marketplace', 'Standard', 'Casual', 'SKU129', 'Accessories', 'One Size', 'B000129', 'Pending', 4, 'USD', 39.99, 'San Francisco', 'CA', '94101', 'USA', NULL, 'Yes', 'Third-Party', NOW()),
(8, 'ORD008', CURDATE(), 'Pending', 'Amazon FBA', 'Online Store', 'Standard', 'Casual', 'SKU130', 'Clothing', 'L', 'B000130', 'Shipped', 2, 'USD', 99.99, 'Denver', 'CO', '80201', 'USA', NULL, 'No', 'Amazon', NOW()),
(9, 'ORD009', CURDATE(), 'Delivered', 'Amazon FBM', 'Wholesale', 'Express', 'Formal', 'SKU131', 'Clothing', 'XL', 'B000131', 'Delivered', 1, 'USD', 109.99, 'Boston', 'MA', '02101', 'USA', NULL, 'Yes', 'Third-Party', NOW()),
(10, 'ORD010', CURDATE(), 'Shipped', 'Amazon FBA', 'Retail', 'Overnight', 'Casual', 'SKU132', 'Footwear', '9', 'B000132', 'Delivered', 3, 'USD', 149.99, 'Dallas', 'TX', '75201', 'USA', NULL, 'No', 'Amazon', NOW());


SELECT COUNT(*) FROM orders_1 WHERE created_at BETWEEN '2025-02-12 07:46:40' AND '2025-02-12 07:47:44'; -- 1760