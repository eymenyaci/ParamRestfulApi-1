using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace WebApi.Services
{
    public class BookService : IBookService
    {
        public async Task<Book> CreateBook(Book book)
        {
            using (var myDbContext = new MyDbContext())
            {
                bool isAnyBook = myDbContext.Books.Any(x => x.Id == book.Id);
                if (isAnyBook is false)
                    await myDbContext.Books.AddAsync(book);
                await myDbContext.SaveChangesAsync();
                return book;
            }
        }

        public async Task DeleteBook(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                var deletedBook = await GetBookById(id);
                if (deletedBook is not null)
                    myDbContext.Books.Remove(deletedBook);
                await myDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            using (var myDbContext = new MyDbContext())
            {
                return await myDbContext.Books.ToListAsync();
            }
        }

        public async Task<Book> GetBookById(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                return await myDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<Book> UpdateBook(Book book)
        {
            using (var myDbContext = new MyDbContext())
            {
                var updatedBook = await GetBookById(book.Id);
                if (updatedBook is not null)
                {
                    updatedBook.Title = book.Title;
                    updatedBook.GenreId = book.GenreId;
                    updatedBook.PageCount = book.PageCount;
                    updatedBook.PublishDate = book.PublishDate;
                    updatedBook.AuthorId = book.AuthorId;
                }
                myDbContext.Books.Update(updatedBook);
                await myDbContext.SaveChangesAsync();
                return updatedBook;
            }
        }
        
    }
}