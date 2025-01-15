namespace Test
{
    public static class Query
    {
        public static readonly string CreateTableQuery = @"
                CREATE TABLE IF NOT EXISTS orders (
                    `index` int,
                    Order_ID VARCHAR(254),                   -- Maximum size for order IDs
                    Date VARCHAR(50),                        -- Dates stored as strings for flexibility
                    Status BLOB,                     -- Status with descriptive names
                    Fulfilment VARCHAR(255),                 -- Fulfillment type
                    Sales_Channel VARCHAR(255),              -- Sales channel description
                    Ship_Service_Level VARCHAR(255),         -- Shipping service level description
                    Style VARCHAR(255),                      -- Style information
                    SKU BLOB,                        -- Stock Keeping Unit identifier
                    Category VARCHAR(255),                   -- Product category
                    Size VARCHAR(100),                       -- Size description
                    ASIN VARCHAR(255),                       -- Amazon Standard Identification Number
                    Courier_Status VARCHAR(255),             -- Courier status description
                    Qty INT,                                 -- Quantity of items
                    Currency VARCHAR(10),                    -- Currency code
                    Amount DECIMAL(18, 2),                   -- Monetary amount with two decimal places
                    Ship_City BLOB,                  -- City of shipment
                    Ship_State BLOB,                 -- State of shipment
                    Ship_Postal_Code VARCHAR(100),            -- Postal code
                    Ship_Country VARCHAR(100),               -- Country of shipment
                    Promotion_IDs BLOB,                      -- Promotion IDs (can store multiple IDs as text)
                    B2B VARCHAR(10),                             -- True/False for B2B flag
                    Fulfilled_By VARCHAR(255)               -- Fulfilled by whom
                                               );

            ";

        public static string DropDatabase = "DROP DATABASE";
    }
}