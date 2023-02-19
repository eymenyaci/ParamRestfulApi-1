using BookStore.Test.TestSetup;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Context;

namespace BookStore.Test
{
    public class CommonTextFixture
    {
        public BookStoreDbContext Context { get; set; }
        public DbContextOptions<BookStoreDbContext> InMemoryDbContextOptions { get; }

        public CommonTextFixture()
        {
            InMemoryDbContextOptions = new DbContextOptionsBuilder<BookStoreDbContext>()
                .UseInMemoryDatabase(databaseName: "BookStoreTestDb")
                .Options;

            Context = new BookStoreDbContext(InMemoryDbContextOptions);
            Context.AddAuthors();
            Context.Database.EnsureCreated();
            Context.SaveChanges();
        }
    }
}
