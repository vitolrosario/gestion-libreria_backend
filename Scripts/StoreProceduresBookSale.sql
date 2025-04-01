-- Get Book Sale by ID
CREATE PROCEDURE sp_GetBookSales
    @ClientId INT = NULL
AS
BEGIN
    SELECT bs.id, bs.ClientId, bs.Date, c.Name, c.Identification, c.Phone, c.Address
    FROM BookSales bs
    INNER JOIN Clients c ON bs.ClientId = c.Id
    WHERE (@ClientId IS NULL OR bs.Id = @ClientId);
END;

-- Add Book Sale (with output parameter)
CREATE PROCEDURE sp_AddBookSale
    @ClientId INT,
    @Date DATETIME,
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO BookSales (ClientId, Date)
    VALUES (@ClientId, @Date);

    SET @Id = SCOPE_IDENTITY();
END;

-- Add Book Sale Detail (with output parameter)
CREATE PROCEDURE sp_AddBookSaleDetail
    @BookSaleId INT,
    @BookId INT,
    @Price DECIMAL(18,2),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO BookSalesDetails (BookSaleId, BookId, Price)
    VALUES (@BookSaleId, @BookId, @Price);

    SET @Id = SCOPE_IDENTITY();
END;
