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
            using (var bookDbContext = new BookDbContext())
            {
                bool isAnyBook = bookDbContext.Books.Any(x => x.Id == book.Id);
                if (!isAnyBook)
                    await bookDbContext.Books.AddAsync(book);
                await bookDbContext.SaveChangesAsync();
                return book;
            }
        }

        public async Task DeleteBook(int id)
        {
            using (var bookDbContext = new BookDbContext())
            {
                var deletedBook = await GetBookById(id);
                if (deletedBook != null)
                    bookDbContext.Books.Remove(deletedBook);
                await bookDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Book>> GetAllBooks()
        {
            using (var bookDbContext = new BookDbContext())
            {
                return await bookDbContext.Books.ToListAsync();
            }
        }

        public async Task<Book> GetBookById(int id)
        {
            using (var bookDbContext = new BookDbContext())
            {
                return await bookDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<Book> UpdateBook(Book book)
        {
            using (var bookDbContext = new BookDbContext())
            {
                var updatedBook = await GetBookById(book.Id);
                if (updatedBook != null)
                {
                    updatedBook.Author = book.Author;
                    updatedBook.BookName = book.BookName;
                    updatedBook.PageCount = book.PageCount;
                }
                bookDbContext.Books.Update(updatedBook);
                await bookDbContext.SaveChangesAsync();
                return updatedBook;
            }
        }
    }
}