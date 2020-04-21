using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Models;

namespace OnlineLibrary.Data
{
    public class OnlineLibraryContext : DbContext
    {
         public OnlineLibraryContext(DbContextOptions<OnlineLibraryContext> options) : base(options)
        {

        }
        public DbSet<Book> Book { get; set; }
        public DbSet<Review> Review { get; set; }
    }
}
