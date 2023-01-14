using BookStoreApi.Data;
using BookStoreApi.Models;
using MongoFramework;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IMongoDbConnection>(sp =>
    MongoDbConnection.FromConnectionString(builder.Configuration.GetConnectionString("BookStoreDbConnection")));
builder.Services.AddTransient<BookStoreDbContext>();
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

app.MapGet("/books", (IBookRepository bookRepository) =>
{
    var books = bookRepository.GetAllBooks();
    return Results.Ok(books);
})
.WithName("Get all books")
.WithOpenApi();

app.MapPost("/books", (IBookRepository bookRepository, Book book) =>
{
    var createdBook = bookRepository.AddBook(book);
    return Results.Created($"/books/{book.Id}", createdBook);
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

    bookRepository.UpdateBook(bookToUpdate, book);
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
