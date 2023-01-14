using BookStoreApi.Models;

public interface IBookRepository
{
    IEnumerable<Book> GetAllBooks();
    Book GetBookById(string bookId);
    Book AddBook(Book book);
    void UpdateBook(Book bookToUpdate, Book book);
    void DeleteBook(Book book);
}
