using AutoMapper;
using Features.Books;
using Features.BookSales;

namespace Features.Clients
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookSale, BookSaleDto>().ReverseMap();
            CreateMap<BookSaleDetail, BookSaleDetailDto>().ReverseMap();
        }
    }
}
