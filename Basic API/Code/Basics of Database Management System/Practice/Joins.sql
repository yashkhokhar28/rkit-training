-- Get all borrowed books along with the borrower’s name. (INNER JOIN)
SELECT 
	b.Title AS BookTitle,
    m.Name AS BorrowerName,
    br.BorrowDate 
FROM 
	Borrow br
JOIN 
	Books b ON br.BookID = b.BookID
JOIN 
	Members m ON br.MemberID = m.MemberID;
    
-- Get all members and the books they borrowed (if any).
SELECT 
	m.Name AS MemberName, b.Title AS BookTitle, br.BorrowDate 
FROM 
	Members m
LEFT JOIN
	Borrow br ON m.MemberID = br.MemberID
LEFT JOIN 
	Books b ON br.BookID = b.BookID;
    
-- Get all borrow records and their corresponding member names.
SELECT 
	br.BorrowID, m.Name AS MemberName, b.Title AS BookTitle 
FROM 
	Borrow br
RIGHT JOIN 
	Members m ON br.MemberID = m.MemberID
RIGHT JOIN 
	Books b ON br.BookID = b.BookID;
    
-- Get all possible pairs of members and books.
SELECT 
	m.Name AS MemberName, b.Title AS BookTitle 
FROM 
	Members m
CROSS JOIN 
	Books b;    