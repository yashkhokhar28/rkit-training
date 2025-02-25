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

INSERT INTO CNT01 (T01F02, T01F03, T01F04, T01F05, T01F06) VALUES
('Aarav', 'Sharma', 'aarav.sharma@example.com', '9123456789', '123 MG Road, Mumbai, Maharashtra'),
('Vivaan', 'Mehta', 'vivaan.mehta@example.com', '9123456790', '456 MG Road, Delhi, Delhi'),
('Aditya', 'Gupta', 'aditya.gupta@example.com', '9123456791', '789 MG Road, Bangalore, Karnataka'),
('Vihaan', 'Singh', 'vihaan.singh@example.com', '9123456792', '101 MG Road, Chennai, Tamil Nadu'),
('Arjun', 'Kumar', 'arjun.kumar@example.com', '9123456793', '202 MG Road, Hyderabad, Telangana'),
('Sai', 'Reddy', 'sai.reddy@example.com', '9123456794', '303 MG Road, Pune, Maharashtra'),
('Ayaan', 'Patel', 'ayaan.patel@example.com', '9123456795', '404 MG Road, Ahmedabad, Gujarat'),
('Krishna', 'Joshi', 'krishna.joshi@example.com', '9123456796', '505 MG Road, Kolkata, West Bengal'),
('Ishaan', 'Nair', 'ishaan.nair@example.com', '9123456797', '606 MG Road, Kochi, Kerala'),
('Harsh', 'Desai', 'harsh.desai@example.com', '9123456798', '707 MG Road, Jaipur, Rajasthan'),
('Atharv', 'Chopra', 'atharv.chopra@example.com', '9123456799', '808 MG Road, Lucknow, Uttar Pradesh'),
('Aarush', 'Chauhan', 'aarush.chauhan@example.com', '9123456800', '909 MG Road, Bhopal, Madhya Pradesh'),
('Anirudh', 'Yadav', 'anirudh.yadav@example.com', '9123456801', '1010 MG Road, Patna, Bihar'),
('Arnav', 'Raj', 'arnav.raj@example.com', '9123456802', '1111 MG Road, Chandigarh, Chandigarh'),
('Rudra', 'Jain', 'rudra.jain@example.com', '9123456803', '1212 MG Road, Bhubaneswar, Odisha'),
('Dev', 'Agarwal', 'dev.agarwal@example.com', '9123456804', '1313 MG Road, Indore, Madhya Pradesh'),
('Kabir', 'Verma', 'kabir.verma@example.com', '9123456805', '1414 MG Road, Guwahati, Assam'),
('Arjun', 'Bose', 'arjun.bose@example.com', '9123456806', '1515 MG Road, Surat, Gujarat'),
('Rohan', 'Mishra', 'rohan.mishra@example.com', '9123456807', '1616 MG Road, Amritsar, Punjab'),
('Dhruv', 'Rao', 'dhruv.rao@example.com', '9123456808', '1717 MG Road, Varanasi, Uttar Pradesh'),
('Aaditya', 'Ghosh', 'aaditya.ghosh@example.com', '9123456809', '1818 MG Road, Nagpur, Maharashtra'),
('Aaryan', 'Das', 'aaryan.das@example.com', '9123456810', '1919 MG Road, Coimbatore, Tamil Nadu'),
('Aryan', 'Dutta', 'aryan.dutta@example.com', '9123456811', '2020 MG Road, Vishakhapatnam, Andhra Pradesh'),
('Siddharth', 'Sen', 'siddharth.sen@example.com', '9123456812', '2121 MG Road, Ludhiana, Punjab'),
('Vivaan', 'Roy', 'vivaan.roy@example.com', '9123456813', '2222 MG Road, Nashik, Maharashtra'),
('Ayaan', 'Sarkar', 'ayaan.sarkar@example.com', '9123456814', '2323 MG Road, Meerut, Uttar Pradesh'),
('Krishna', 'Paul', 'krishna.paul@example.com', '9123456815', '2424 MG Road, Jodhpur, Rajasthan'),
('Ishaan', 'Chatterjee', 'ishaan.chatterjee@example.com', '9123456816', '2525 MG Road, Jabalpur, Madhya Pradesh'),
('Harsh', 'Bhattacharya', 'harsh.bhattacharya@example.com', '9123456817', '2626 MG Road, Raipur, Chhattisgarh'),
('Atharv', 'Majumdar', 'atharv.majumdar@example.com', '9123456818', '2727 MG Road, Ranchi, Jharkhand'),
('Aarush', 'Mukherjee', 'aarush.mukherjee@example.com', '9123456819', '2828 MG Road, Gwalior, Madhya Pradesh'),
('Anirudh', 'Saha', 'anirudh.saha@example.com', '9123456820', '2929 MG Road, Goa, Goa'),
('Arnav', 'Chakraborty', 'arnav.chakraborty@example.com', '9123456821', '3030 MG Road, Mysore, Karnataka'),
('Rudra', 'Basu', 'rudra.basu@example.com', '9123456822', '3131 MG Road, Dehradun, Uttarakhand'),
('Dev', 'Ganguly', 'dev.ganguly@example.com', '9123456823', '3232 MG Road, Shillong, Meghalaya'),
('Kabir', 'Lal', 'kabir.lal@example.com', '9123456824', '3333 MG Road, Tiruchirappalli, Tamil Nadu'),
('Arjun', 'Bansal', 'arjun.bansal@example.com', '9123456825', '3434 MG Road, Kota, Rajasthan'),
('Rohan', 'Bajaj', 'rohan.bajaj@example.com', '9123456826', '3535 MG Road, Bareilly, Uttar Pradesh'),
('Dhruv', 'Khan', 'dhruv.khan@example.com', '9123456827', '3636 MG Road, Aligarh, Uttar Pradesh'),
('Aaditya', 'Kapoor', 'aaditya.kapoor@example.com', '9123456828', '3737 MG Road, Bhavnagar, Gujarat'),
('Aaryan', 'Shetty', 'aaryan.shetty@example.com', '9123456829', '3838 MG Road, Bilaspur, Chhattisgarh'),
('Aryan', 'Menon', 'aryan.menon@example.com', '9123456830', '3939 MG Road, Udaipur, Rajasthan'),
('Siddharth', 'Iyer', 'siddharth.iyer@example.com', '9123456831', '4040 MG Road, Siliguri, West Bengal'),
('Vivaan', 'Pillai', 'vivaan.pillai@example.com', '9123456832', '4141 MG Road, Jammu, Jammu and Kashmir'),
('Ayaan', 'Nambiar', 'ayaan.nambiar@example.com', '9123456833', '4242 MG Road, Thiruvananthapuram, Kerala'),
('Krishna', 'Narayan', 'krishna.narayan@example.com', '9123456834', '4343 MG Road, Warangal, Telangana'),
('Ishaan', 'Acharya', 'ishaan.acharya@example.com', '9123456835', '4444 MG Road, Guntur, Andhra Pradesh'),
('Harsh', 'Pandey', 'harsh.pandey@example.com', '9123456836', '4545 MG Road, Hubli, Karnataka');

