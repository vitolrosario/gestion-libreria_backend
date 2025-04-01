using Dapper;
using Features.Books;
using Features.Clients;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Features.BookSales
{
    public class BookSaleRepository : IBookSaleRepository
    {
        private readonly string _connectionString;

        public BookSaleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("db");
        }

        public async Task<IEnumerable<BookSale>>GetBookSalesAsync(int? clientId, DateTime? startDate, DateTime? endDate)
        {

            var test = new DateTime(2022, 1, 1);
            using var connection = new SqlConnection(_connectionString);
            var bookSalesDictionary = new Dictionary<int, BookSale>();

            var bookSales = await connection.QueryAsync<BookSale, Client, BookSaleDetail, BookSale>(
                "sp_GetBookSales",
                (bookSale, client, bookSaleDetail) =>
                {
                    // Check if the book sale already exists
                    if (!bookSalesDictionary.TryGetValue(bookSale.Id, out var existingBookSale))
                    {
                        existingBookSale = bookSale;
                        existingBookSale.Client = client;
                        existingBookSale.Client.Id = client.Client_Id;
                        existingBookSale.Details = new List<BookSaleDetail>();
                        bookSalesDictionary.Add(bookSale.Id, existingBookSale);
                    }

                    // Add details if not null
                    if (bookSaleDetail != null) 
                    {
                        bookSaleDetail.Book = new Book { Id = bookSaleDetail.TemporalBookId };
                        existingBookSale.Details.Add(bookSaleDetail);
                    }

                    return existingBookSale;
                },
                new { ClientId = clientId, StartDate = startDate != null ? startDate.ToString() : null, EndDate = endDate != null ? endDate.ToString() : null },
                splitOn: "ClientId, BookSaleDetailId",  
                commandType: CommandType.StoredProcedure
            );

            return bookSales;
        }

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
