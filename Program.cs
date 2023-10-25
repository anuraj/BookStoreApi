using BookStoreApi.Data;
using BookStoreApi.Models;
using BookStoreApi.Repositories;
using BookStoreApi.ViewModels;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MongoDbConnection")
    ?? throw new InvalidOperationException("Connection string is null");

var databaseName = builder.Configuration.GetConnectionString("MongoDbDatabaseName") ?? "BooksStoreDb";
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseMongoDB(connectionString, databaseName));
builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/books", async (IBookRepository bookRepository, CancellationToken cancellationToken) =>
{
    var books = await bookRepository.GetAllBooks(cancellationToken);
    return Results.Ok(books);
})
.WithName("Get all books")
.WithOpenApi();

app.MapPost("/books", (IBookRepository bookRepository, Book book) =>
{
    var bookEntity = new BookEntity
    {
        Author = book.Author,
        Category = book.Category,
        Name = book.Name,
        Price = book.Price
    };

    var createdBook = bookRepository.AddBook(bookEntity);
    return Results.Created($"/books/{bookEntity.Id}", createdBook);
})
.WithName("Create a book")
.WithOpenApi();

app.MapGet("/books/{id}", (IBookRepository bookRepository, string id) =>
{
    var book = bookRepository.GetBookById(id);
    return book == null ? Results.NotFound() : Results.Ok(book);
}).WithName("Get a book")
.WithOpenApi();

app.MapPut("/books/{id}", (IBookRepository bookRepository, string id, Book book) =>
{
    var bookToUpdate = bookRepository.GetBookById(id);
    if (bookToUpdate == null)
    {
        return Results.NotFound();
    }

    var bookEntity = new BookEntity
    {
        Author = book.Author,
        Category = book.Category,
        Name = book.Name,
        Price = book.Price
    };
    bookRepository.UpdateBook(bookToUpdate, bookEntity);
    return Results.NoContent();
}).WithName("Update a book")
.WithOpenApi();

app.MapDelete("/books/{id}", (IBookRepository bookRepository, BookStoreDbContext bookStoreDbContext, string id) =>
{
    var book = bookRepository.GetBookById(id);
    if (book == null)
    {
        return Results.NotFound();
    }

    bookRepository.DeleteBook(book);
    return Results.NoContent();
}).WithName("Delete a book")
.WithOpenApi();


app.Run();
