using Library_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_WebApi.Context
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Book> Books { get; set; }
    }
}