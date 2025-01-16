explain analyze select fulfilled_by from orders where fulfilled_by = 'યશ';
explain select count(*) from orders where fulfilled_by = 'યશ';

CREATE INDEX idx_order_id ON orders(Order_ID);

-- index for filtering column that is location based
CREATE INDEX idx_location ON orders(Ship_Country(100), Ship_State(100), Ship_City(100));

-- useful for querying based on fulfillment type and sales channel, especially when filtering by amount
CREATE INDEX idx_fulfillment_channel_amount ON orders(Fulfilment, Sales_Channel, Amount);

-- since status and B2B flag can often be queried together to get a specific status of orders in B2B transactions
CREATE INDEX idx_status_b2b_date ON orders(Status, B2B, Date);


-- filtered 10% without index
-- filtered 100% with index idx_order_id
explain SELECT * FROM orders
WHERE Order_ID = '408-7955685-3083534';

-- filtered 0.10% without index
-- filtered 100% row with index idx_location
explain SELECT * FROM orders
WHERE Ship_Country = 'IN'
AND Ship_State = 'MAHARASHTRA'
AND Ship_City = 'MUMBAI';

-- filtered 1% using force index 
explain SELECT * FROM orders
FORCE INDEX (idx_order_id)
WHERE Ship_Country = 'IN'
AND Ship_State = 'MAHARASHTRA'
AND Ship_City = 'MUMBAI';

-- filtered 100% row with index idx_fulfillment_channel_amount
explain SELECT * FROM orders
FORCE INDEX (idx_fulfillment_channel_amount)
WHERE Fulfilment = 'Standard'
AND Sales_Channel = 'Online'
AND Amount > 100;

