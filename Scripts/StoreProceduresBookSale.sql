-- Get Book Sale by ID
CREATE PROCEDURE sp_GetBookSales
    @ClientId INT = NULL,
    @StartDate DATETIME = NULL,
    @EndDate DATETIME = NULL
AS
BEGIN
   SELECT bs.Id AS Id,
          bs.Date, 
          c.Id AS ClientId,    
          c.Id as Client_id,
          c.Name, 
          c.Identification, 
          c.Phone, 
          c.Address,
          bsd.Id AS BookSaleDetailId,
		  bsd.BookId AS TemporalBookId,
		  bsd.Price
    FROM BookSales bs
    INNER JOIN Clients c ON bs.ClientId = c.Id
	INNER JOIN BookSalesDetails bsd on bsd.BookSaleId = bs.Id
    WHERE (@ClientId IS NULL OR bs.Id = @ClientId)
    AND ((@StartDate IS NULL AND @EndDate IS NULL) OR CONVERT(DATE, bs.Date) BETWEEN CONVERT(DATE, @StartDate) AND CONVERT(DATE, @EndDate));
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
