using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Interfaces;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace WebApi.Services
{
    public class GenreService : IGenreService
    {
        public async Task<Genre> CreateGenre(Genre genre)
        {
            using (var myDbContext = new MyDbContext())
            {
                bool isAnyGenre = myDbContext.Genres.Any(x => x.Id == genre.Id);
                if (isAnyGenre is false)
                    await myDbContext.Genres.AddAsync(genre);
                await myDbContext.SaveChangesAsync();
                return genre;
            }
        }

        public async Task DeleteGenre(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                var deletedGenre = await GetGenreById(id);
                if (deletedGenre is not null)
                    myDbContext.Genres.Remove(deletedGenre);
                await myDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            using (var myDbContext = new MyDbContext())
            {
                return await myDbContext.Genres.ToListAsync();
            }
        }

        public async Task<Genre> GetGenreById(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                return await myDbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
            using (var myDbContext = new MyDbContext())
            {
                var updatedGenre = await GetGenreById(genre.Id);
                if (updatedGenre is not null)
                {
                    updatedGenre.Name = genre.Name;
                    updatedGenre.IsActive = genre.IsActive;
                }
                myDbContext.Genres.Update(updatedGenre);
                await myDbContext.SaveChangesAsync();
                return updatedGenre;
            }
        }

        public bool IsAnyGenre(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                return myDbContext.Genres.Any(x => x.Id == id);
            }
        }
    }
}
