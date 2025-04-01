namespace Features.BookSales
{
    public interface IBookSaleRepository
    {
        Task<IEnumerable<BookSale>> GetBookSalesAsync(int? clientId, DateTime? startDate, DateTime? endDate);
        Task<int> AddBookSaleAsync(BookSale bookSale);
    }
}
