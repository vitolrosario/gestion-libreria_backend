using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Features.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public BookRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        // Get All Books
        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<Book>(
                "sp_GetAllBooks", 
                commandType: CommandType.StoredProcedure
            );
        }

        // Get Book by ID
        public async Task<Book> GetBookByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<Book>(
                "sp_GetBookById",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
        }

        // Add Book (with output parameter)
        public async Task<int> AddBookAsync(Book book)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Name", book.Name);
            parameters.Add("@Year", book.Year);
            parameters.Add("@Author", book.Author);
            parameters.Add("@Editorial", book.Editorial);
            parameters.Add("@Price", book.Price);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_AddBook", 
                parameters, 
                commandType: CommandType.StoredProcedure
            );

            return parameters.Get<int>("@Id"); 
        }

        // Update Book
        public async Task<bool> UpdateBookAsync(Book book)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(
                "sp_UpdateBook",
                new { book.Id, book.Name, book.Year, book.Author, book.Editorial, book.Price },
                commandType: CommandType.StoredProcedure
            );
            return rowsAffected > 0;
        }

        // Delete Book
        public async Task<bool> DeleteBookAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var rowsAffected = await connection.ExecuteAsync(
                "sp_DeleteBook",
                new { Id = id },
                commandType: CommandType.StoredProcedure
            );
            return rowsAffected > 0;
        }
    }
}
