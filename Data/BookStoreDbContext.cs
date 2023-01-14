using BookStoreApi.Models;
using MongoFramework;

namespace BookStoreApi.Data
{
    public class BookStoreDbContext : MongoDbContext
    {
        public BookStoreDbContext(IMongoDbConnection connection) : base(connection)
        {
        }
        public MongoDbSet<Book> Books { get; set; } = null!;
    }
}