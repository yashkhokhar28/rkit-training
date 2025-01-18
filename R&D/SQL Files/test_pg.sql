explain analyze select fulfilled_by from orders where fulfilled_by = 'યશ';
explain select fulfilled_by from orders where fulfilled_by = 'યશ';

CREATE INDEX idx_order_id ON orders(Order_ID);

CREATE INDEX idx_location ON orders(Ship_Country, Ship_State, Ship_City);

CREATE INDEX idx_fulfillment_channel_amount ON orders(Fulfilment, Sales_Channel, Amount);

CREATE INDEX idx_status_b2b_date ON orders(Status, B2B, Date);

-- used index idx_order_id
explain ANALYSE SELECT * FROM orders
WHERE Order_ID = '408-7955685-3083534';

-- used idx_location
explain SELECT * FROM orders
WHERE Ship_Country = 'IN'
AND Ship_State = 'MAHARASHTRA'
AND Ship_City = 'MUMBAI';

SET enable_seqscan = OFF;
-- used index idx_location
explain SELECT * FROM orders
WHERE Ship_Country = 'IN'
AND Ship_State = 'MAHARASHTRA'
AND Ship_City = 'MUMBAI';
SET enable_seqscan = ON;


-- filtered 100% row with index idx_fulfillment_channel_amount
explain SELECT * FROM orders
FORCE INDEX (idx_fulfillment_channel_amount)
WHERE Fulfilment = 'Standard'
AND Sales_Channel = 'Online'
AND Amount > 100;
