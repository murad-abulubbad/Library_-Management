-- =============================================
-- ?? Library Database Structure (By: Murad Abulubbad)
-- =============================================

USE master;
GO

IF DB_ID('LibraryDB') IS NULL
    CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

-- =============================================
-- 1?? TABLES
-- =============================================

-- ?? Books
CREATE TABLE Books (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(200) NOT NULL,
    Year INT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- ?? Categories
CREATE TABLE Categories (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- ?? BookCategories (Many-to-Many)
CREATE TABLE BookCategories (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    BookId BIGINT NOT NULL,
    CategoryId BIGINT NOT NULL,
    CONSTRAINT FK_BookCategories_Book FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    CONSTRAINT FK_BookCategories_Category FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);



-- =============================================
-- 3?? STORED PROCEDURE
-- =============================================

IF OBJECT_ID('sp_GetAllBooksWithCategories', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetAllBooksWithCategories;
GO

CREATE PROCEDURE sp_GetAllBooksWithCategories
AS
BEGIN
    SELECT
        b.Id AS BookId,
        b.Title,
        b.Author,
        b.Year,
        c.Id AS CategoryId,
        c.Name AS CategoryName
    FROM Books b
    LEFT JOIN BookCategories bc ON b.Id = bc.BookId
    LEFT JOIN Categories c ON bc.CategoryId = c.Id
    ORDER BY b.Id;
END;
GO
