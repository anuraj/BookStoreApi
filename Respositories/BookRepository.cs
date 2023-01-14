using BookStoreApi.Data;
using BookStoreApi.Models;

public class BookRepository : IBookRepository
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    public BookRepository(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;
    }
    public Book AddBook(Book book)
    {
        _bookStoreDbContext.Books.Add(book);
        _bookStoreDbContext.SaveChanges();
        return book;
    }

    public void DeleteBook(Book book)
    {
        _bookStoreDbContext.Books.Remove(book);
        _bookStoreDbContext.SaveChanges();
    }

    public IEnumerable<Book> GetAllBooks()
    {
        var books = _bookStoreDbContext.Books.ToList();
        return books;
    }

    public Book GetBookById(string bookId)
    {
        var book = _bookStoreDbContext.Books.Find(bookId);
        return book;
    }

    public void UpdateBook(Book bookToUpdate, Book book)
    {
        bookToUpdate.Name = book.Name;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Price = book.Price;
        bookToUpdate.Category = book.Category;
        _bookStoreDbContext.SaveChanges();
    }
}
