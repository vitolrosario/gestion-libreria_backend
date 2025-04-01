using Dapper;
using Features.Clients;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Features.BookSales
{
    public class BookSaleRepository : IBookSaleRepository
    {
        private readonly string _connectionString;

        public BookSaleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        // Get All BookSales
        public async Task<IEnumerable<BookSale>>GetBookSalesAsync(int? clientId)
        {
            using var connection = new SqlConnection(_connectionString);
            
            var bookSales = await connection.QueryAsync<BookSale, Client, BookSale>(
                "sp_GetBookSales",
                (bookSale, client) =>
                {
                    bookSale.Client = client; 
                    bookSale.Client.Id = client.Id;
                    return bookSale;
                },
                new { ClientId = clientId },
                splitOn: "ClientId", 
                commandType: CommandType.StoredProcedure
            );

            return bookSales;
        }

        // Add BookSale (with output parameter)
        public async Task<int> AddBookSaleAsync(BookSale bookSale)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@ClientId", bookSale.Client.Id);
            parameters.Add("@Date", bookSale.Date);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "sp_AddBookSale", 
                parameters, 
                commandType: CommandType.StoredProcedure
            );

            var bookSaleId = parameters.Get<int>("@Id");

            foreach (var detail in bookSale.Details)
            {
                var detailParameters = new DynamicParameters();
                detailParameters.Add("@BookSaleId", bookSaleId);
                detailParameters.Add("@BookId", detail.Book.Id);
                detailParameters.Add("@Price", detail.Price);
                detailParameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync(
                    "sp_AddBookSaleDetail", 
                    detailParameters, 
                    commandType: CommandType.StoredProcedure
                );
            }

            return parameters.Get<int>("@Id");
        }
    }
}
