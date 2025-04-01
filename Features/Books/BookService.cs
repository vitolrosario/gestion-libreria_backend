using AutoMapper;

namespace Features.Books
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return await _bookRepository.GetAllBooksAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetBookByIdAsync(id);
        }

        public async Task<int> AddBookAsync(BookDto bookDto)
        {
            var book = _mapper.Map<Book>(bookDto);
            return await _bookRepository.AddBookAsync(book);
        }

        public async Task<bool> UpdateBookAsync(int id, BookDto bookDto)
        {
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null) return false;

            _mapper.Map(bookDto, book);
            return await _bookRepository.UpdateBookAsync(book);
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
        }
    }
}
