using BookStore.Api.Interfaces;
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
        private readonly IBookStoreDbContext _bookStoreDbContext;

        public BookService(IBookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<Book> CreateBook(Book book)
        {
            bool isAnyBook = _bookStoreDbContext.Books.Any(x => x.Id == book.Id);
            if (isAnyBook is false)
                await _bookStoreDbContext.Books.AddAsync(book);
            _bookStoreDbContext.SaveChanges();
            return book;
        }

        public async Task DeleteBook(int id)
        {
            var deletedBook = await GetBookById(id);
            if (deletedBook is not null)
                _bookStoreDbContext.Books.Remove(deletedBook);
            _bookStoreDbContext.SaveChanges();
        }

        public async Task<List<Book>> GetAllBooks()
        {
            return await _bookStoreDbContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _bookStoreDbContext.Books.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> UpdateBook(Book book)
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
            _bookStoreDbContext.Books.Update(updatedBook);
            _bookStoreDbContext.SaveChanges();
            return updatedBook;
        }

        public async Task<Book> GetBookByAuthorId(int authorId)
        {
            var book = await _bookStoreDbContext.Books.FirstOrDefaultAsync(x => x.AuthorId == authorId);
            return book;
        }

        public async Task<Book> GetBookByGenreId(int genreId)
        {
            var book = await _bookStoreDbContext.Books.FirstOrDefaultAsync(x => x.GenreId == genreId);
            return book;
        }

    }
}