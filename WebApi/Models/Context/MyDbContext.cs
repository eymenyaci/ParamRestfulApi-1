using Microsoft.EntityFrameworkCore;
using System;
using WebApi.Models.Entity;

namespace WebApi.Models.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Author> Authors{ get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JFVV9NF;Database=BookDB;User Id=sa;Password=eymen123");
        }

    }


}