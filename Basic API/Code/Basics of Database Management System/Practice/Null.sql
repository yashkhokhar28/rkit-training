-- Find members who haven’t returned books:
SELECT 
	m.Name, b.Title 
FROM
	Members m
JOIN
	Borrow br ON m.MemberID = br.MemberID
JOIN 
	Books b ON br.BookID = b.BookID
WHERE
	br.ReturnDate IS NULL;