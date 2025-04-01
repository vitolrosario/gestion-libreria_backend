namespace Features.BookSales
{
    public interface IBookSaleService
    {
        Task<IEnumerable<BookSale>> GetBookSalesAsync(int? clientId, DateTime? startDate, DateTime? endDate);
        Task<int> AddBookSaleAsync(BookSaleDto bookSaleDto);
    }
}
