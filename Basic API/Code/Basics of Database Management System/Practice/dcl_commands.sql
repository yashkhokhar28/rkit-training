-- GRANT 
-- Grant SELECT and INSERT privileges on the 'Student' table to user 'JohnDoe'
GRANT SELECT, INSERT ON Student TO JohnDoe;

-- Grant all privileges on the 'Country' table to 'AdminUser'
GRANT ALL PRIVILEGES ON Country TO AdminUser;

-- Allow 'ManagerRole' to grant SELECT on 'City' to others
GRANT SELECT ON City TO ManagerRole WITH GRANT OPTION;

-- REVOKE

-- Revoke INSERT privilege on 'Student' table from 'JohnDoe'
REVOKE INSERT ON Student FROM JohnDoe;

-- Revoke all privileges on 'Country' table from 'AdminUser'
REVOKE ALL PRIVILEGES ON Country FROM AdminUser;