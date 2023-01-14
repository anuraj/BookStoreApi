namespace BookStoreApi.Models;

public class Book
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Author { get; set; }
    public string? Category { get; set; }
    public decimal Price { get; set; }
}