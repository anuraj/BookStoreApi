using BookStoreApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApi.Data
{
    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext(DbContextOptions options) : base(options)
        {
        }

        protected BookStoreDbContext()
        {
        }

        public DbSet<BookEntity> Books { get; set; } = null!;
    }
}