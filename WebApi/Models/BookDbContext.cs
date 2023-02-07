using Microsoft.EntityFrameworkCore;
using System;

namespace WebApi
{
    public class BookDbContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-JFVV9NF;Database=BookDB;User Id=sa;Password=eymen123");
        }

    }


}