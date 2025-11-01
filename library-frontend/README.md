⚙️ Setup Instructions

Clone the Repository
git clone https://github.com/yourusername/LibraryManagementSystem.git
Open Library.sln in Visual Studio 2022
Set Library.API as the startup project
Create a SQL Server database named LibraryDB
Run the provided table + stored procedure scripts from the DatabaseScripts folder
cd Library.API
dotnet run
Swagger UI
Run FrontEnd
cd library-frontend
npm install
ng serve
////////////////////////
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
## 🕐 Time Estimation
Task	Estimated Time	Actual Time Spent
Backend (Books + Categories CRUD + Stored Procedure)	1-2 Dys
Frontend (Angular CRUD + ADO.NET integration) 2-3	!Days
Testing, Debugging & Documentation		0.5 day
## ⚙️ Challenges Faced

During this project, I faced several challenges while ensuring a clean architecture and seamless user experience:

Working with ADO.NET and Stored Procedures for the first time
This was the most challenging part for me. At the beginning, I had to spend time understanding how ADO.NET works, how to execute stored procedures, pass parameters, and map the returned data correctly into DTOs. However, after reading documentation and trying examples, I was able to clearly understand how ADO.NET integrates with EF Core inside the same architecture.

Managing the Many-to-Many Relationship (Books ↔ Categories)
Handling the relationship in both the backend and the UI required careful design. The challenge was to correctly load category lists, allow selecting/unselecting categories from the UI, and update the relationship without duplications or data loops.

Avoiding Cyclic References in DTOs
Since Books contain Categories, and Categories can contain Books, I had to shape the DTOs in a way that prevents circular references and infinite JSON loops. This required using DTOs instead of returning entity models directly.

Creating Reusable Angular Material Forms
Although I have previous experience with Angular, I was more familiar with using ready-made components. In this project, I focused on building the forms myself — especially the category multi-select form used when adding or editing a book.
## AI
During the development process, I used several AI tools (such as ChatGPT, Copilot,).
The main goal was not to rely on them for code generation, but to learn and understand the concepts deeply — especially since the time frame for the task was short.
Each tool provided different insights, and I explored multiple ones to find the most accurate explanations and best architectural practices.
