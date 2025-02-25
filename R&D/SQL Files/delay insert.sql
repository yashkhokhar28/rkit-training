DELIMITER $$

CREATE PROCEDURE InsertOrdersWithTransaction(
    IN p_Limit INT,   -- Number of records to insert
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
    DECLARE commit_counter INT DEFAULT 0;

    -- Start the transaction
    START TRANSACTION;

    WHILE counter < p_Limit DO
        -- Get next available index
        SELECT COALESCE(MAX(`index`), 0) + 1 INTO next_index FROM orders_1;

        -- Insert a single record
        INSERT INTO orders_1 (
            `index`, Order_ID, Date, Status, Fulfilment, Sales_Channel, Ship_Service_Level,
            Style, SKU, Category, Size, ASIN, Courier_Status, Qty, Currency, Amount,
            Ship_City, Ship_State, Ship_Postal_Code, Ship_Country, Promotion_IDs, B2B, Fulfilled_By
        ) VALUES (
            next_index,
            CONCAT(p_Order_ID, '-', counter + 1), -- Unique Order_ID
            p_Date, p_Status, p_Fulfilment, p_Sales_Channel, p_Ship_Service_Level, 
            p_Style, p_SKU, p_Category, p_Size, p_ASIN, p_Courier_Status, p_Qty, 
            p_Currency, p_Amount, p_Ship_City, p_Ship_State, p_Ship_Postal_Code, 
            p_Ship_Country, p_Promotion_IDs, p_B2B, p_Fulfilled_By
        );

        -- Increment counters
        SET counter = counter + 1;
        SET commit_counter = commit_counter + 1;

        -- Sleep for 1 second
        DO SLEEP(1);

        -- Commit every 10 inserts
        IF commit_counter = 10 THEN
            COMMIT;
            START TRANSACTION;
            SET commit_counter = 0;
        END IF;
    END WHILE;

    -- Commit any remaining records
    COMMIT;
END $$

DELIMITER ;


CALL InsertOrdersWithTransaction(
    100,  -- Number of rows to insert
    'ORD12345', '2025-02-17', 'Shipped', 'Amazon', 'Online', 'Standard',
    'Casual', 'SKU001', 'Clothing', 'L', 'B00012345', 'In Transit', 2, 'USD', 49.99,
    'New York', 'NY', '10001', 'USA', 'PROMO2025', 'Yes', 'Amazon Warehouse'
);




SELECT COUNT(*) FROM orders_1; 

DROP PROCEDURE InsertOrdersWithTransaction;

-- Add primary key constraint to the `index` column
ALTER TABLE test_db_1.orders_86
ADD PRIMARY KEY (`index`);

-- Create an index on the `index` column (if not already indexed)
CREATE INDEX idx_index ON test_db_1.orders_86(`index`);

-- Verify the distinct count of the `index` column
SELECT COUNT(DISTINCT `index`) FROM test_db_1.orders_86;
