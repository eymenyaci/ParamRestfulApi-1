using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.Services
{
    public class BookService : IBookService
    {
        public Book CreateBook(Book book)
        {
            using (var BookDbContext = new BookDbContext())
            {
                bool isAnyBook = BookDbContext.Books.Any(x => x.Id == book.Id);
                if (!isAnyBook)
                    BookDbContext.Books.Add(book);
                BookDbContext.SaveChanges();
                return book;
            }
        }

        public void DeleteBook(int id)
        {
            using (var BookDbContext = new BookDbContext())
            {
                var deletedBook = GetBookById(id);
                if (deletedBook != null)
                    BookDbContext.Books.Remove(deletedBook);
                BookDbContext.SaveChanges();
            }
        }

        public List<Book> GetAllBooks()
        {
            using (var BookDbContext = new BookDbContext())
            {
                return BookDbContext.Books.ToList();
            }
        }

        public Book GetBookById(int id)
        {
            using (var BookDbContext = new BookDbContext())
            {
                return BookDbContext.Books.FirstOrDefault(x => x.Id == id);
            }
        }

        public Book UpdateBook(Book book)
        {
            using (var BookDbContext = new BookDbContext())
            {
                var updatedBook = GetBookById(book.Id);
                if (updatedBook != null)
                {
                    updatedBook.Author = book.Author;
                    updatedBook.BookName = book.BookName;
                    updatedBook.PageCount = book.PageCount;
                }
                BookDbContext.Books.Update(updatedBook);
                BookDbContext.SaveChanges();
                return updatedBook;
            }
        }
    }
}