using BookStoreApi.Models;

namespace BookStoreApi.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<BookEntity>> GetAllBooks(CancellationToken cancellationToken = default);
        BookEntity? GetBookById(string bookId);
        BookEntity AddBook(BookEntity book);
        void UpdateBook(BookEntity bookToUpdate, BookEntity book);
        void DeleteBook(BookEntity book);
    }
}