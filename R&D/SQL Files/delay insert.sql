DELIMITER $$

CREATE PROCEDURE InsertOrdersLoop(
    IN p_Limit INT,   -- Number of records to insert (should be a multiple of 10 for each cycle)
    IN p_Order_ID VARCHAR(254),
    IN p_Date VARCHAR(50),
    IN p_Status BLOB,
    IN p_Fulfilment VARCHAR(255),
    IN p_Sales_Channel VARCHAR(255),
    IN p_Ship_Service_Level VARCHAR(255),
    IN p_Style VARCHAR(255),
    IN p_SKU BLOB,
    IN p_Category VARCHAR(255),
    IN p_Size VARCHAR(100),
    IN p_ASIN VARCHAR(255),
    IN p_Courier_Status VARCHAR(255),
    IN p_Qty INT,
    IN p_Currency VARCHAR(10),
    IN p_Amount DECIMAL(18, 2),
    IN p_Ship_City BLOB,
    IN p_Ship_State BLOB,
    IN p_Ship_Postal_Code VARCHAR(100),
    IN p_Ship_Country VARCHAR(100),
    IN p_Promotion_IDs BLOB,
    IN p_B2B VARCHAR(10),
    IN p_Fulfilled_By VARCHAR(255)
)
BEGIN
    DECLARE counter INT DEFAULT 0;
    DECLARE next_index INT;
    DECLARE cycle_counter INT;

    -- Calculate how many cycles are needed for the given limit (each cycle inserts 10 records)
    SET cycle_counter = CEIL(p_Limit / 10);

    -- Loop through each cycle (for every 10 records)
    WHILE cycle_counter > 0 DO
        SET counter = 0;

        -- Loop to insert 10 records
        WHILE counter < 10 AND counter < p_Limit DO
            -- Get next available index
            SELECT COALESCE(MAX(`index`), 0) + 1 INTO next_index FROM orders_1;

            -- Insert record with the new index value
            INSERT INTO orders_1 (
                `index`, Order_ID, Date, Status, Fulfilment, Sales_Channel, Ship_Service_Level,
                Style, SKU, Category, Size, ASIN, Courier_Status, Qty, Currency, Amount,
                Ship_City, Ship_State, Ship_Postal_Code, Ship_Country, Promotion_IDs, B2B, Fulfilled_By
            ) VALUES (
                next_index,
                CONCAT(p_Order_ID, '-', counter + 1), -- Append counter to make unique Order_ID
                p_Date, p_Status, p_Fulfilment, p_Sales_Channel, p_Ship_Service_Level, 
                p_Style, p_SKU, p_Category, p_Size, p_ASIN, p_Courier_Status, p_Qty, 
                p_Currency, p_Amount, p_Ship_City, p_Ship_State, p_Ship_Postal_Code, 
                p_Ship_Country, p_Promotion_IDs, p_B2B, p_Fulfilled_By
            );

            -- Increase counter for the current cycle
            SET counter = counter + 1;
        END WHILE;

        -- Wait for 0.1 seconds (10 records per second)
        DO SLEEP(1);

        -- Decrease cycle counter
        SET cycle_counter = cycle_counter - 1;
    END WHILE;

END $$

DELIMITER ;

-- To test the procedure
CALL InsertOrdersLoop(
    2500, -- Number of rows to insert (in multiples of 10)
    'ORD12345', '2025-02-17', 'Shipped', 'Amazon', 'Online', 'Standard',
    'Casual', 'SKU001', 'Clothing', 'L', 'B00012345', 'In Transit', 2, 'USD', 49.99,
    'New York', 'NY', '10001', 'USA', 'PROMO2025', 'Yes', 'Amazon Warehouse'
);

-- Drop the procedure after use
DROP PROCEDURE InsertOrdersLoop;

-- Check the number of records in the table
SELECT COUNT(*) FROM orders_1; -- Check how many records were inserted
