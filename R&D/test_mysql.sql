select count(*) from orders;

-- Index on Order_ID: This will improve query performance when searching or joining by the Order_ID.
CREATE INDEX idx_order_id ON orders (Order_ID);

-- Index on Date: This will speed up queries that filter by date, especially if you're querying orders over a specific range of dates.
CREATE INDEX idx_date ON orders (Date);

-- Composite Index on Status and Amount: If you frequently query orders based on their status and amount, this composite index will improve performance for such queries.
CREATE INDEX idx_status_amount ON orders (Status(100), Amount);

-- Composite Index on Date and Ship_City: This composite index is useful if you're filtering by date and shipping location (city) in queries, which is a common use case in e-commerce platforms.
CREATE INDEX idx_date_ship_city ON orders (Date, Ship_City(100));
CREATE INDEX idx_ship_city ON orders (Ship_City(100));
-- without create index

-- '77152' record
EXPLAIN
SELECT count(*) 
FROM orders
WHERE Amount > 100
AND Status = 'Shipped';
# id, select_type, table, partitions, type, possible_keys, key, key_len, ref, rows, filtered, Extra
-- '1', 'SIMPLE', 'orders', NULL, 'ALL', NULL, NULL, NULL, NULL, '130655', '3.33', 'Using where'


-- '3942' record
EXPLAIN
SELECT count(*) 
FROM orders where Ship_City = 'MUMBAI'
  AND Date >= '04-30-22';
# id, select_type, table, partitions, type, possible_keys, key, key_len, ref, rows, filtered, Extra
-- '1', 'SIMPLE', 'orders', NULL, 'ALL', NULL, NULL, NULL, NULL, '130655', '3.33', 'Using where'

-- with index

-- '77152' record
EXPLAIN
SELECT count(*) 
FROM orders
WHERE Amount > 100
AND Status = 'Shipped';
# id, select_type, table, partitions, type, possible_keys, key, key_len, ref, rows, filtered, Extra
-- '1', 'SIMPLE', 'orders', NULL, 'ALL', 'idx_status_amount', NULL, NULL, NULL, '130655', '50.00', 'Using where'


-- '3942' record
EXPLAIN
SELECT count(*) 
FROM orders where Ship_City = 'MUMBAI'
  AND Date >= '04-30-22';
# id, select_type, table, partitions, type, possible_keys, key, key_len, ref, rows, filtered, Extra
-- '1', 'SIMPLE', 'orders', NULL, 'ALL', 'idx_date,idx_date_ship_city', NULL, NULL, NULL, '130655', '5.00', 'Using where'



SELECT count(*) 
FROM orders
force index (idx_date_ship_city)
WHERE Ship_City = 'MUMBAI'
  AND Date >= '04-30-22';
  
SELECT count(*) 
FROM orders
force index (idx_ship_city)
WHERE Ship_City = 'MUMBAI'
  AND Date >= '04-30-22';
  
SELECT count(*) 
FROM orders
force index (idx_date)
WHERE Ship_City = 'MUMBAI'
  AND Date >= '04-30-22';