-- Get All Clients
CREATE PROCEDURE sp_GetAllClients
AS
BEGIN
    SELECT Id, Name, Identification, Phone, Address, Status FROM Clients WHERE Status = 1;
END;

-- Get Client by ID
CREATE PROCEDURE sp_GetClientById
    @Id INT
AS
BEGIN
    SELECT Id, Name, Identification, Phone, Address, Status FROM Clients WHERE Id = @Id AND Status = 1;
END;

-- Add Client (with Output Parameter)
CREATE PROCEDURE sp_AddClient
    @Name NVARCHAR(100),
    @Identification NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(100),
    @Id INT OUTPUT
AS
BEGIN
    INSERT INTO Clients (Name, Identification, Phone, Address, Status)
    VALUES (@Name, @Identification, @Phone, @Address, 1);

    SET @Id = SCOPE_IDENTITY();
END;

-- Update Client
CREATE PROCEDURE sp_UpdateClient
    @Id INT,
    @Name NVARCHAR(100),
    @Identification NVARCHAR(100),
    @Phone NVARCHAR(20),
    @Address NVARCHAR(100)
AS
BEGIN
    UPDATE Clients
    SET Name = @Name, Identification = @Identification, Phone = @Phone, Address = @Address
    WHERE Id = @Id;
END;

-- Update Client To Deleted Status
CREATE PROCEDURE sp_DeleteClient
    @Id INT
AS
BEGIN
    UPDATE Clients SET Status = 0 WHERE Id = @Id;
END;
