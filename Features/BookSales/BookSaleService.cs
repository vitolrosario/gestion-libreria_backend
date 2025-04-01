using AutoMapper;
using Features.Books;
using Features.Clients;

namespace Features.BookSales
{
    public class BookSaleService : IBookSaleService
    {
        private readonly IBookSaleRepository _bookSaleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookSaleService(
            IBookSaleRepository bookSaleRepository, 
            IClientRepository clientRepository, 
            IBookRepository bookRepository,
            IMapper mapper)
        {
            _bookSaleRepository = bookSaleRepository;
            _clientRepository = clientRepository;
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookSale>> GetBookSalesAsync(int? clientId)
        {
            return await _bookSaleRepository.GetBookSalesAsync(clientId);
        }

        public async Task<int> AddBookSaleAsync(BookSaleDto bookSaleDto)
        {
            var bookSale = _mapper.Map<BookSale>(bookSaleDto);
            // bookSale.Client = await _clientRepository.GetClientByIdAsync(bookSaleDto.ClientId);
            // foreach (var detailDto in bookSaleDto.Details)
            // {
            //     var detail = _mapper.Map<BookSaleDetail>(detailDto);
            //     bookSale.Details.Add(detail);
            //     // detail.Book = await _bookRepository.GetBookByIdAsync(detailDto.BookId);
            // }
            return await _bookSaleRepository.AddBookSaleAsync(bookSale);
        }
    }
}
