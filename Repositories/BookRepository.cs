using BookStoreApi.Data;
using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

namespace BookStoreApi.Repositories;

public class BookRepository : IBookRepository
{
    private readonly BookStoreDbContext _bookStoreDbContext;
    public BookRepository(BookStoreDbContext bookStoreDbContext)
    {
        _bookStoreDbContext = bookStoreDbContext;
    }
    public BookEntity AddBook(BookEntity book)
    {
        book.Id = ObjectId.GenerateNewId();
        _bookStoreDbContext.Books.Add(book);
        _bookStoreDbContext.SaveChanges();
        return book;
    }

    public void DeleteBook(BookEntity book)
    {
        _bookStoreDbContext.Books.Remove(book);
        _bookStoreDbContext.SaveChanges();
    }

    public async Task<IEnumerable<BookEntity>> GetAllBooks(CancellationToken cancellationToken = default)
    {
        var books = await _bookStoreDbContext.Books.ToListAsync(cancellationToken);
        return books;
    }

    public BookEntity? GetBookById(string bookId)
    {
        var book = _bookStoreDbContext.Books.Find(ObjectId.Parse(bookId));
        return book;
    }

    public void UpdateBook(BookEntity bookToUpdate, BookEntity book)
    {
        bookToUpdate.Name = book.Name;
        bookToUpdate.Author = book.Author;
        bookToUpdate.Price = book.Price;
        bookToUpdate.Category = book.Category;
        _bookStoreDbContext.SaveChanges();
    }
}