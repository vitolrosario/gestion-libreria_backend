using Features.Clients;

namespace Features.BookSales
{
    public class BookSale
    {
        public int Id { get; set; }
        public Client Client { get; set; }
        public DateTime Date { get; set; }
        public List<BookSaleDetail> Details { get; set; }
    }
}
