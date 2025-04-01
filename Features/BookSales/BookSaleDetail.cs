using Features.Books;

namespace Features.BookSales
{
    public class BookSaleDetail
    {
        public int Id { get; set; }
        public BookSale BookSale { get; set; }
        public Book Book { get; set; }
        public double Price { get; set; }
    }
}
