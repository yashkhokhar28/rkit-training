-- SELECT 

-- Select All Columns
SELECT 
	* 
FROM 
	Books;

-- Select Specific Columns
SELECT 
	Title,
    Author,
    Price
FROM 
	Books;

-- Select With Conditions
SELECT 
	Name,
    Email 
FROM 
	Members 
WHERE 
	JoinDate > '2024-12-01';

-- Select With Sorting
SELECT 
	Title,
    Price 
FROM 
	Books 
ORDER BY 
	Price DESC;

-- Select With Aggregation
SELECT 
	Genre,
    AVG(Price) AS AvgPrice 
FROM 
	Books 
GROUP BY 
	Genre;

-- Select With Join
SELECT 
	b.Title, m.Name AS Borrower, br.BorrowDate 
FROM 
	Borrow br
JOIN 
	Books b ON br.BookID = b.BookID
JOIN 
	Members m ON br.MemberID = m.MemberID;

-- Select With Subquery
SELECT 
	Title 
FROM 
	Books 
WHERE 
	Price > (SELECT
				AVG(Price)
			FROM 
				Books);