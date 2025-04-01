-- Get All Books
CREATE PROCEDURE sp_GetAllBooks
AS
BEGIN
    SELECT Id, Name, Year, Author, Editorial, Price, Status FROM Books WHERE Status = 1;
END;

-- Get Book by ID
CREATE PROCEDURE sp_GetBookById
    @Id INT
AS
BEGIN
    SELECT Id, Name, Year, Author, Editorial, Price, Status FROM Books WHERE Id = @Id AND Status = 1;
END;

-- Add Book (with output parameter)
CREATE PROCEDURE sp_AddBook
    @Name NVARCHAR(100),
    @Year INT,
    @Author NVARCHAR(100),
    @Editorial NVARCHAR(100),
    @Price DECIMAL(18,2),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Books (Name, Year, Author, Editorial, Price, Status)
    VALUES (@Name, @Year, @Author, @Editorial, @Price, 1);

    SET @Id = SCOPE_IDENTITY(); 
END;

-- Update Book
CREATE PROCEDURE sp_UpdateBook
    @Id INT,
    @Name NVARCHAR(100),
    @Year INT,
    @Author NVARCHAR(100),
    @Editorial NVARCHAR(100),
    @Price DECIMAL(18,2)
AS
BEGIN
    UPDATE Books
    SET Name = @Name, Year = @Year, Author = @Author, Editorial = @Editorial, Price = @Price
    WHERE Id = @Id;
END;

-- Update Book To Deleted Status
CREATE PROCEDURE sp_DeleteBook
    @Id INT
AS
BEGIN
    UPDATE Books SET Status = 0 WHERE Id = @Id;
END;
