using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace BookStore.Api.Models.Context
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(
                    new Author { Id = 1, Name = "J.K.", Surname = "Rowling", DateOfBirth = new DateTime(1965, 7, 31) },
                    new Author { Id = 2, Name = "Stephen", Surname = "King", DateOfBirth = new DateTime(1947, 9, 21) },
                    new Author { Id = 3, Name = "Harper", Surname = "Lee", DateOfBirth = new DateTime(1926, 4, 28) }
                    );
                }
                if (!context.Genres.Any())
                {
                    context.Genres.AddRange(
                        new Genre { Id = 1, Name = "Fantasy", IsActive = true },
                        new Genre { Id = 2, Name = "Horror", IsActive = true },
                        new Genre { Id = 3, Name = "Classic", IsActive = true }
                        );
                }
                if (!context.Books.Any())
                {
                    context.Books.AddRange(
                    new Book { Id = 1, Title = "Harry Potter and the Philosopher's Stone", GenreId = 1, PageCount = 223, PublishDate = new DateTime(1997, 6, 26), AuthorId = 1 },
                    new Book { Id = 2, Title = "Carrie", GenreId = 2, PageCount = 199, PublishDate = new DateTime(1974, 4, 5), AuthorId = 2 },
                    new Book { Id = 3, Title = "To Kill a Mockingbird", GenreId = 3, PageCount = 281, PublishDate = new DateTime(1960, 7, 11), AuthorId = 3 }
                    );
                }

                context.SaveChanges();






                
            }
        }
    }
}
