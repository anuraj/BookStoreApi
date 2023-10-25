using MongoDB.Bson;

namespace BookStoreApi.Models;

public class BookEntity
{
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
}