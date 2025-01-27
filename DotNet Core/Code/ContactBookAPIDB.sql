-- Create Database
CREATE DATABASE ContactBookDB;

-- Use the Database
USE ContactBookDB;

select * from CNT01;

-- Create Contacts Table
CREATE TABLE CNT01 (
    T01F01 INT AUTO_INCREMENT PRIMARY KEY COMMENT "Contact ID",       
    T01F02 VARCHAR(100) NOT NULL COMMENT "First Name",         
    T01F03 VARCHAR(100) NOT NULL COMMENT "Last Name",          
    T01F04 VARCHAR(255) NOT NULL UNIQUE COMMENT "Email Address",      
    T01F05 VARCHAR(15) COMMENT "Phone Number",                 
    T01F06 VARCHAR(255) COMMENT "Address",                    
    T01F07 DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT "Created Date", 
    T01F08 DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT "Modified Date"  
);
