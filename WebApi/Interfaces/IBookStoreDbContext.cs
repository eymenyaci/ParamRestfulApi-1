using Microsoft.EntityFrameworkCore;
using WebApi.Models.Entity;

namespace BookStore.Api.Interfaces
{
    public interface IBookStoreDbContext
    {
        DbSet<Log> Logs { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }
        DbSet<Genre> Genres { get; set; }
        int SaveChanges();
    }
}
