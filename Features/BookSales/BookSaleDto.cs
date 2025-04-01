namespace Features.BookSales
{
    public class BookSaleDto
    {
        public int ClientId { get; set; }
        public DateTime Date { get; set; }
        public List<BookSaleDetailDto> Details { get; set; }
    }
}
