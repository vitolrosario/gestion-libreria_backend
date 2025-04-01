namespace Features.BookSales
{
    public interface IBookSaleService
    {
        Task<IEnumerable<BookSale>> GetBookSalesAsync(int? clientId);
        Task<int> AddBookSaleAsync(BookSaleDto bookSaleDto);
    }
}
