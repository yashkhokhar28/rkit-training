CREATE DATABASE StockDB;
USE StockDB;
DROP DATABASE StockDB;
SELECT * FROM USR01;
SELECT * FROM STK01;
SELECT * FROM PRT01;
SELECT * FROM TRN01;
SELECT * FROM PTS01;
-- Create Users Table
DROP TABLE USR01;
CREATE TABLE USR01 (
    R01F01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'UserId: Unique identifier for each user',
    R01F02 VARCHAR(100) NOT NULL COMMENT 'Username: The name chosen by the user for login',
    R01F03 VARCHAR(255) NOT NULL UNIQUE COMMENT 'Email: The unique email address of the user',
    R01F04 VARCHAR(255) NOT NULL COMMENT 'PasswordHash: The hashed password for the user',
    R01F05 VARCHAR(255) NOT NULL DEFAULT "User" COMMENT 'Role: Role Of The User',
    R01F06 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'CreatedAt: Timestamp when the user was created',
    R01F07 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'UpdatedAt: Timestamp when the user details were last updated'
);

-- Create Stocks Table
CREATE TABLE STK01 (
    K01F01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'StockId: Unique identifier for each stock',
    K01F02 VARCHAR(20) NOT NULL UNIQUE COMMENT 'StockSymbol: The stock ticker symbol (e.g., AAPL for Apple)',
    K01F03 VARCHAR(255) NOT NULL COMMENT 'StockName: The full name of the stock (e.g., Apple Inc.)',
    K01F04 DECIMAL(18, 2) NOT NULL COMMENT 'Price: The current price of the stock',
    K01F05 DECIMAL(18, 2) COMMENT 'MarketCap: The market capitalization of the stock (optional)',
    K01F06 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'CreatedAt: Timestamp when the stock was added',
    K01F07 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'UpdatedAt: Timestamp when the stock details were last updated'
);
drop table PTS01;
CREATE TABLE PTS01 (
    S01F01 INT AUTO_INCREMENT PRIMARY KEY,          -- Unique ID for each portfolio stock entry
    S01F02 INT NOT NULL,                            -- User ID (FK to USR01)
    S01F03 INT NOT NULL,                            -- Stock ID (FK to STK01)
    S01F04 INT NOT NULL DEFAULT 0,                  -- Quantity of stock in the portfolio
    S01F05 DECIMAL(18, 2) NOT NULL,                 -- Purchase price of the stock
    S01F06 DECIMAL(18, 2) NOT NULL,                 -- Current value of the stock
    S01F07 DECIMAL(18, 2) NOT NULL,                 -- Total value of this stock in the portfolio (calculated)
    S01F08 TIMESTAMP DEFAULT CURRENT_TIMESTAMP,     -- Created at
    S01F09 TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- Updated at
    FOREIGN KEY (S01F02) REFERENCES USR01(R01F01),  -- Link to User table
    FOREIGN KEY (S01F03) REFERENCES STK01(K01F01)  -- Link to Stock table
);




-- Create Portfolios Table
CREATE TABLE PRT01 (
    T01F01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'PortfolioId: Unique identifier for each portfolio',
    T01F02 INT COMMENT 'UserId: Foreign key referencing the User who owns the portfolio',
    T01F03 VARCHAR(100) NOT NULL COMMENT 'PortfolioName: The name of the portfolio (e.g., "My First Portfolio")',
    T01F04 DECIMAL(18, 2) DEFAULT 0 COMMENT 'TotalValue: The total value of the portfolio (calculated based on stocks)',
    T01F05 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'CreatedAt: Timestamp when the portfolio was created',
    T01F06 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'UpdatedAt: Timestamp when the portfolio details were last updated',
    FOREIGN KEY (T01F02) REFERENCES USR01(R01F01)
);

-- Create Transactions Table
CREATE TABLE TRN01 (
    N01F01 INT AUTO_INCREMENT PRIMARY KEY COMMENT 'TransactionId: Unique identifier for each transaction',
    N01F02 INT COMMENT 'UserId: Foreign key referencing the user who made the transaction',
    N01F03 INT COMMENT 'StockId: Foreign key referencing the stock involved in the transaction',
    N01F04 INT NOT NULL COMMENT 'Quantity: The number of shares bought or sold in the transaction',
    N01F05 DECIMAL(18, 2) NOT NULL COMMENT 'PriceAtTransaction: The price of the stock at the time of the transaction',
    N01F06 VARCHAR(20) NOT NULL COMMENT 'TransactionType: The type of transaction, either "Buy" or "Sell"',
    N01F07 TIMESTAMP DEFAULT CURRENT_TIMESTAMP COMMENT 'TransactionDate: Timestamp when the transaction occurred',
    FOREIGN KEY (N01F02) REFERENCES USR01(R01F01),  -- UserId reference to the user who initiated the transaction
    FOREIGN KEY (N01F03) REFERENCES STK01(K01F01)  -- StockId reference to the stock involved in the transaction
);
DROP TABLE TRN01;

