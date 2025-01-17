namespace Test
{
    public static class Query
    {
        // MySQL Create Table Query
        public static readonly string CreateTableQueryMySQL = @"
            CREATE TABLE IF NOT EXISTS orders (
                `index` INT,                              -- Unique index for each row
                Order_ID VARCHAR(254),                    -- Maximum size for order IDs
                Date VARCHAR(50),                         -- Dates stored as strings for flexibility
                Status BLOB,                              -- Status with descriptive names
                Fulfilment VARCHAR(255),                 -- Fulfillment type
                Sales_Channel VARCHAR(255),              -- Sales channel description
                Ship_Service_Level VARCHAR(255),         -- Shipping service level description
                Style VARCHAR(255),                      -- Style information
                SKU BLOB,                                 -- Stock Keeping Unit identifier
                Category VARCHAR(255),                   -- Product category
                Size VARCHAR(100),                       -- Size description
                ASIN VARCHAR(255),                       -- Amazon Standard Identification Number
                Courier_Status VARCHAR(255),             -- Courier status description
                Qty INT,                                  -- Quantity of items
                Currency VARCHAR(10),                    -- Currency code
                Amount DECIMAL(18, 2),                   -- Monetary amount with two decimal places
                Ship_City BLOB,                           -- City of shipment
                Ship_State BLOB,                          -- State of shipment
                Ship_Postal_Code VARCHAR(100),           -- Postal code
                Ship_Country VARCHAR(100),               -- Country of shipment
                Promotion_IDs BLOB,                      -- Promotion IDs (can store multiple IDs as text)
                B2B VARCHAR(10),                         -- True/False for B2B flag
                Fulfilled_By VARCHAR(255)                -- Fulfilled by whom
            );
        ";

        // PostgreSQL Create Table Query
        public static readonly string CreateTableQueryPostgreSQL = @"
            CREATE TABLE IF NOT EXISTS orders (
                index INT,                               -- Unique index for each row
                Order_ID VARCHAR(254),                   -- Maximum size for order IDs
                Date VARCHAR(50),                        -- Dates stored as strings for flexibility
                Status VARCHAR(254),                     -- Status with descriptive names
                Fulfilment VARCHAR(255),                 -- Fulfillment type
                Sales_Channel VARCHAR(255),              -- Sales channel description
                Ship_Service_Level VARCHAR(255),         -- Shipping service level description
                Style VARCHAR(255),                      -- Style information
                SKU VARCHAR(254),                        -- Stock Keeping Unit identifier
                Category VARCHAR(255),                   -- Product category
                Size VARCHAR(100),                       -- Size description
                ASIN VARCHAR(255),                       -- Amazon Standard Identification Number
                Courier_Status VARCHAR(255),             -- Courier status description
                Qty INT,                                 -- Quantity of items
                Currency VARCHAR(10),                    -- Currency code
                Amount NUMERIC(18, 2),                   -- Monetary amount with two decimal places
                Ship_City VARCHAR(254),                  -- City of shipment
                Ship_State VARCHAR(254),                 -- State of shipment
                Ship_Postal_Code VARCHAR(100),           -- Postal code
                Ship_Country VARCHAR(100),               -- Country of shipment
                Promotion_IDs VARCHAR(65535),            -- Promotion IDs (can store multiple IDs as text)
                B2B VARCHAR(10),                         -- True/False for B2B flag
                Fulfilled_By VARCHAR(255)                -- Fulfilled by whom
            );
        ";

        // MS SQL Server Create Table Query
        public static readonly string CreateTableQueryMSSQL = @"
            IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='orders' AND xtype='U')
            CREATE TABLE orders (
                [index] INT,                             -- Unique index for each row
                [Order_ID] NVARCHAR(254),                -- Maximum size for order IDs
                [Date] NVARCHAR(50),                     -- Dates stored as strings for flexibility
                [Status] NVARCHAR(MAX),                  -- Status with descriptive names
                [Fulfilment] NVARCHAR(255),              -- Fulfillment type
                [Sales_Channel] NVARCHAR(255),           -- Sales channel description
                [Ship_Service_Level] NVARCHAR(255),      -- Shipping service level description
                [Style] NVARCHAR(255),                   -- Style information
                [SKU] NVARCHAR(MAX),                     -- Stock Keeping Unit identifier
                [Category] NVARCHAR(255),                -- Product category
                [Size] NVARCHAR(100),                    -- Size description
                [ASIN] NVARCHAR(255),                    -- Amazon Standard Identification Number
                [Courier_Status] NVARCHAR(255),          -- Courier status description
                [Qty] INT,                               -- Quantity of items
                [Currency] NVARCHAR(10),                 -- Currency code
                [Amount] DECIMAL(18, 2),                 -- Monetary amount with two decimal places
                [Ship_City] NVARCHAR(MAX),               -- City of shipment
                [Ship_State] NVARCHAR(MAX),              -- State of shipment
                [Ship_Postal_Code] NVARCHAR(100),        -- Postal code
                [Ship_Country] NVARCHAR(100),            -- Country of shipment
                [Promotion_IDs] NVARCHAR(MAX),           -- Promotion IDs (can store multiple IDs as text)
                [B2B] NVARCHAR(10),                      -- True/False for B2B flag
                [Fulfilled_By] NVARCHAR(255)             -- Fulfilled by whom
            );
        ";

        // PostgreSQL Drop Database Query
        //public static readonly string DropDatabasePostgreSQL = @"
        //            SELECT pg_terminate_backend(pid)
        //            FROM pg_stat_activity
        //            WHERE datname = {0} AND pid <> pg_backend_pid();";

        // MySQL Drop Database Query
        public static readonly string DropDatabaseMySQL = "DROP DATABASE IF EXISTS `{0}`;";

        // MS SQL Server Drop Database Query
        public static readonly string DropDatabaseMSSQL = @"
            IF EXISTS (SELECT name FROM sys.databases WHERE name = '{0}')
            BEGIN
                ALTER DATABASE [{0}] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                DROP DATABASE [{0}];
            END";
    }
}