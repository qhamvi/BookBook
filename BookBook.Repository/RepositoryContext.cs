using BookBook.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BookBook.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Author> Authors {get; set;}
        public DbSet<Book> Books {get; set;}

    }
}