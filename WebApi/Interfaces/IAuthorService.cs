using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.Entity;

namespace WebApi.Interfaces
{
    public interface IAuthorService
    {
        Task<List<Author>> GetAllAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> CreateAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task DeleteAuthor(int id);
    }
}
