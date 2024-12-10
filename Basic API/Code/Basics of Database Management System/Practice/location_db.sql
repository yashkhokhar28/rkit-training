-- Create database and switch to it
CREATE DATABASE location_db;
USE location_db;

-- Drop database if it already exists (use with caution)
DROP DATABASE location_db;

-- Table for countries
CREATE TABLE Country (
    CountryID INT AUTO_INCREMENT PRIMARY KEY,
    CountryName VARCHAR(15) NOT NULL,
    CountryCode VARCHAR(7) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Table for states with a foreign key to Country
CREATE TABLE State (
	StateID	INT AUTO_INCREMENT PRIMARY KEY,
    CountryID INT,
    StateName VARCHAR(15) NOT NULL,
    StateCode VARCHAR(7) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CountryID) REFERENCES Country(CountryID)
); 

-- Table for cities with a foreign key to State
CREATE TABLE City (
	CityID	INT AUTO_INCREMENT PRIMARY KEY,
    StateID INT,
    CityName VARCHAR(15) NOT NULL,
    CityCode VARCHAR(7) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    ModifiedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (StateID) REFERENCES State(StateID)
);

-- Table for students with foreign keys to Country, State, and City
CREATE TABLE Student (
    StudentID INT AUTO_INCREMENT PRIMARY KEY,
    CountryID INT,
    StateID INT,
    CityID INT,
    FirstName VARCHAR(50) NOT NULL,           
    LastName VARCHAR(50),                     
    Gender ENUM('Male', 'Female', 'Other') NOT NULL, 
    BirthDate DATE NOT NULL,                  
    EnrollmentTime DATETIME DEFAULT CURRENT_TIMESTAMP, 
    GPA DECIMAL(4, 2) CHECK (GPA BETWEEN 0 AND 10), 
    IsFullTime BOOLEAN DEFAULT TRUE,          
    Address TEXT,                             
    PhoneNumber VARCHAR(10),                     
    ProfilePicture BLOB,                      
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CountryID) REFERENCES Country(CountryID),
    FOREIGN KEY (StateID) REFERENCES State(StateID),
    FOREIGN KEY (CityID) REFERENCES City(CityID)
);

-- Insert sample data into Country table
INSERT INTO Country (CountryName, CountryCode) VALUES ('India', 'IN');
INSERT INTO Country (CountryName, CountryCode) VALUES ('United States', 'US');
INSERT INTO Country (CountryName, CountryCode) VALUES ('Canada', 'CA');
INSERT INTO Country (CountryName, CountryCode) VALUES ('Australia', 'AU');
INSERT INTO Country (CountryName, CountryCode) VALUES ('Germany', 'DE');
INSERT INTO Country (CountryName, CountryCode) VALUES ('France', 'FR');
INSERT INTO Country (CountryName, CountryCode) VALUES ('Japan', 'JP');
INSERT INTO Country (CountryName, CountryCode) VALUES ('Brazil', 'BR');
INSERT INTO Country (CountryName, CountryCode) VALUES ('South Africa', 'ZA');
INSERT INTO Country (CountryName, CountryCode) VALUES ('United Kingdom', 'UK');


-- Insert sample data into State table
INSERT INTO State (CountryID, StateName, StateCode) VALUES (1, 'Gujarat', 'GJ');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (1, 'Maharashtra', 'MH');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (2, 'California', 'CA');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (2, 'Texas', 'TX');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (3, 'Ontario', 'ON');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (3, 'Quebec', 'QC');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (4, 'New South Wales', 'NSW');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (4, 'Victoria', 'VIC');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (5, 'Bavaria', 'BY');
INSERT INTO State (CountryID, StateName, StateCode) VALUES (6, 'Île-de-France', 'IDF');

-- Insert sample data into City table
INSERT INTO City (StateID, CityName, CityCode) VALUES (1, 'Ahmedabad', 'AMD');
INSERT INTO City (StateID, CityName, CityCode) VALUES (1, 'Surat', 'SUR');
INSERT INTO City (StateID, CityName, CityCode) VALUES (2, 'Mumbai', 'BOM');
INSERT INTO City (StateID, CityName, CityCode) VALUES (2, 'Pune', 'PUN');
INSERT INTO City (StateID, CityName, CityCode) VALUES (3, 'Los Angeles', 'LA');
INSERT INTO City (StateID, CityName, CityCode) VALUES (3, 'San Francisco', 'SF');
INSERT INTO City (StateID, CityName, CityCode) VALUES (5, 'Toronto', 'TOR');
INSERT INTO City (StateID, CityName, CityCode) VALUES (5, 'Ottawa', 'OTT');
INSERT INTO City (StateID, CityName, CityCode) VALUES (6, 'Montreal', 'MON');
INSERT INTO City (StateID, CityName, CityCode) VALUES (4, 'Sydney', 'SYD');

-- Insert sample data into Student table
INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (1, 1, 1, 'John', 'Doe', 'Male', '2000-01-15', 8.5, '123 Elm St, Ahmedabad', '9876543210');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (1, 1, 2, 'Jane', 'Smith', 'Female', '1999-03-25', 9.1, '456 Maple St, Surat', '9876543211');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (2, 3, 3, 'Alice', 'Brown', 'Female', '2001-07-10', 7.8, '789 Pine St, Los Angeles', '9876543212');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (2, 3, 4, 'Bob', 'Davis', 'Male', '2002-11-22', 6.9, '101 Cedar St, San Francisco', '9876543213');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (3, 5, 7, 'Charlie', 'Miller', 'Male', '1998-05-15', 8.2, '202 Birch St, Toronto', '9876543214');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (3, 5, 8, 'Daisy', 'Wilson', 'Female', '2000-12-05', 9.3, '303 Walnut St, Ottawa', '9876543215');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (4, 7, 10, 'Ella', 'Clark', 'Female', '2001-04-20', 8.7, '404 Poplar St, Sydney', '9876543216');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (5, 9, NULL, 'Frank', 'Lopez', 'Male', '1999-08-18', 7.5, '505 Oak St, Bavaria', '9876543217');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (6, 10, NULL, 'Grace', 'Martinez', 'Female', '2000-09-12', 9.0, '606 Chestnut St, Île-de-France', '9876543218');

INSERT INTO Student (CountryID, StateID, CityID, FirstName, LastName, Gender, BirthDate, GPA, Address, PhoneNumber)
VALUES (1, 2, 2, 'Hank', 'White', 'Other', '2001-06-09', 7.9, '707 Willow St, Mumbai', '9876543219');
 