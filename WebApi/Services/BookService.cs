using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace WebApi.Services
{
    public class BookService : IBookService
    {
        public Book CreateBook(Book book)
        {
            using (var bookDbContext = new BookDbContext())
            {
                bool isAnyBook = bookDbContext.Books.Any(x => x.Id == book.Id);
                if (!isAnyBook)
                    bookDbContext.Books.Add(book);
                bookDbContext.SaveChanges();
                return book;
            }
        }

        public void DeleteBook(int id)
        {
            using (var bookDbContext = new BookDbContext())
            {
                var deletedBook = GetBookById(id);
                if (deletedBook != null)
                    bookDbContext.Books.Remove(deletedBook);
                bookDbContext.SaveChanges();
            }
        }

        public List<Book> GetAllBooks()
        {
            using (var bookDbContext = new BookDbContext())
            {
                return bookDbContext.Books.ToList();
            }
        }

        public Book GetBookById(int id)
        {
            using (var bookDbContext = new BookDbContext())
            {
                return bookDbContext.Books.FirstOrDefault(x => x.Id == id);
            }
        }

        public Book UpdateBook(Book book)
        {
            using (var bookDbContext = new BookDbContext())
            {
                var updatedBook = GetBookById(book.Id);
                if (updatedBook != null)
                {
                    updatedBook.Author = book.Author;
                    updatedBook.BookName = book.BookName;
                    updatedBook.PageCount = book.PageCount;
                }
                bookDbContext.Books.Update(updatedBook);
                bookDbContext.SaveChanges();
                return updatedBook;
            }
        }
    }
}