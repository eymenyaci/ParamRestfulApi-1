using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models.Entity;

namespace WebApi.Interfaces
{
    public interface IBookService
    {
        Task<List<Book>> GetAllBooks();
        Task<Book>GetBookById(int id);
        Task<Book> CreateBook(Book book);
        Task<Book> UpdateBook(Book book);
        Task DeleteBook(int id);
        bool IsAnyAuthor (int authorId);
        bool IsAnyGenre (int genreId);

    }
}
