⚙️ Setup Instructions

1) Clone the Repository
git clone  https://github.com/murad-abulubbad/Library_-Management
cd LibraryManagementSystem
2) Open the Solution in Visual Studio

Open Library.sln using Visual Studio 2022

Set Library.API as the Startup Project

3) Database Setup (SQL Server)

Run the following script in SQL Serve
USE master;
GO

IF DB_ID('LibraryDB') IS NULL
    CREATE DATABASE LibraryDB;
GO

USE LibraryDB;
GO

-- Books Table
CREATE TABLE Books (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(200) NOT NULL,
    Year INT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Categories Table
CREATE TABLE Categories (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(200) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- BookCategories (Many-to-Many)
CREATE TABLE BookCategories (
    Id BIGINT IDENTITY(1,1) PRIMARY KEY,
    BookId BIGINT NOT NULL,
    CategoryId BIGINT NOT NULL,
    CONSTRAINT FK_BookCategories_Book FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    CONSTRAINT FK_BookCategories_Category FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);

-- Stored Procedure to return books with categories
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
cd Library.API
dotnet run
https://localhost:{API_PORT}/swagger
🗄️ Database Connection (Connection String Setup)

After creating the database, you need to configure the SQL Server connection string in the backend.

Open:
Library.API/appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=LibraryDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
🔹 If you're using SQL Server Authentication, use:
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=LibraryDB;User Id=sa;Password=YourPassword;TrustServerCertificate=True;"
}
🔗 Apply Connection String in Program.cs

Ensure EF Core is using the correct DB connectio
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    🌐 Network & API Endpoint Configuration (Frontend → Backend)

Because frontend and backend run on different ports, we define the API URL in Angular.

Open:
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7229/api'
};
Make sure the port matches your backend Swagger URL.
### 🏗️ (Optional) Create Database Using EF Core Migration

Instead of running the SQL script manually, you can generate the database using Entity Framework migrations:

1) Open Package Manager Console
2) Ensure the project containing the DbContext is selected as Default Project
3) Run:
   Add-Migration InitLibrarySchema
4) Then:
   Update-Database

This will automatically create all tables and relationships inside LibraryDB.
################# This not include the Stored Procedure you want run this SQL
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
