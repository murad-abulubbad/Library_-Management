using Library.Application.DTOs.CategoryDTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Library.Infrastructure.Repositories
{
	public class AdoBookRepository
	{
		private readonly string _connectionString;

		public AdoBookRepository(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection");
		}

		public async Task<List<BookWithCategoriesDto>> GetAllBooksWithCategoriesAsync()
		{
			var books = new Dictionary<long, BookWithCategoriesDto>();

			using var connection = new SqlConnection(_connectionString);
			using var command = new SqlCommand("sp_GetAllBooksWithCategories", connection);
			command.CommandType = CommandType.StoredProcedure;

			await connection.OpenAsync();
			using var reader = await command.ExecuteReaderAsync();

			while (await reader.ReadAsync())
			{
				var bookId = reader.GetInt64(reader.GetOrdinal("BookId"));

				if (!books.ContainsKey(bookId))
				{
					books[bookId] = new BookWithCategoriesDto(
						bookId,
						reader.GetString(reader.GetOrdinal("Title")),
						reader.GetString(reader.GetOrdinal("Author")),
						reader.IsDBNull(reader.GetOrdinal("Year")) ? null : reader.GetInt32(reader.GetOrdinal("Year")),
						new List<CategoryDto>()
					);
				}

				if (!reader.IsDBNull(reader.GetOrdinal("CategoryId")))
				{
					books[bookId].Categories.Add(new CategoryDto
					{
						Id = reader.GetInt64(reader.GetOrdinal("CategoryId")),
						Name = reader.GetString(reader.GetOrdinal("CategoryName"))
					});
				}
			}

			return books.Values.ToList();
		}
	}
}
