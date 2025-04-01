namespace Features.Books
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book> GetBookByIdAsync(int id);
        Task<int> AddBookAsync(BookDto bookDto);
        Task<bool> UpdateBookAsync(int id, BookDto bookDto);
        Task DeleteBookAsync(int id);
    }
}
