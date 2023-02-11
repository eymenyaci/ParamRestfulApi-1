using System.Collections.Generic;
using System.Threading.Tasks;
using WebApi.Models.Entity;

namespace WebApi.Interfaces
{
    public interface IGenreService
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre> GetGenreById(int id);
        Task<Genre> CreateGenre(Genre genre);
        Task<Genre> UpdateGenre(Genre genre);
        Task DeleteGenre(int id);

        bool IsAnyGenre(int id);
    }
}
