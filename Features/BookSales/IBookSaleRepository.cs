namespace Features.BookSales
{
    public interface IBookSaleRepository
    {
        Task<IEnumerable<BookSale>> GetBookSalesAsync(int? clientId);
        Task<int> AddBookSaleAsync(BookSale bookSale);
    }
}
