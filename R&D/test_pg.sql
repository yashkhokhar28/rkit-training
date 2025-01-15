SELECT COUNT(*) FROM orders;

-- Index on Order_ID: This will help when filtering or joining by Order_ID.

-- Index on Date: This will speed up queries filtering by Date.
CREATE INDEX idx_date ON orders (Date);

-- Composite Index on Status and Amount: Since you're filtering by Status and Amount, this composite index will improve performance for such queries.
CREATE INDEX idx_status_amount ON orders (Status, Amount);

-- Composite Index on Date and Ship_City: This composite index is useful when filtering by both Date and Ship_City.
CREATE INDEX idx_date_ship_city ON orders (Date, Ship_City);




CREATE INDEX idx_order_id ON orders (Order_ID);
CREATE INDEX idx_date ON orders (Date);
CREATE INDEX idx_ship_city ON orders (ship_city);

set enable_seqscan=false;

explain ANALYSE
  SELECT count(*) 
FROM orders
WHERE Ship_City = 'MUMBAI'
  AND Date >= '04-30-22';

 
ANALYZE;
SET random_page_cost = 1.0;
SET effective_cache_size = '0.1 GB';
