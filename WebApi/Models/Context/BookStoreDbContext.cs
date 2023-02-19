using BookStore.Api.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Models.Entity;

namespace WebApi.Models.Context
{
    public class BookStoreDbContext : DbContext,IBookStoreDbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Log> Logs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors{ get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }


    }


}