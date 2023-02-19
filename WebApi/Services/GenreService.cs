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
    public class GenreService : IGenreService
    {
        private readonly IBookStoreDbContext _bookStoreDbContext;

        public GenreService(IBookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<Genre> CreateGenre(Genre genre)
        {
            bool isAnyGenre = _bookStoreDbContext.Genres.Any(x => x.Name == genre.Name);
            if (isAnyGenre is false)
                await _bookStoreDbContext.Genres.AddAsync(genre);
            else
                throw new InvalidOperationException();
            _bookStoreDbContext.SaveChanges();
            return genre;   
        }

        public async Task DeleteGenre(int id)
        {
            if (id == 0)
                throw new InvalidOperationException();
            var deletedGenre = await GetGenreById(id);
            if (deletedGenre is not null)
                _bookStoreDbContext.Genres.Remove(deletedGenre);
            else throw new ArgumentNullException();
            _bookStoreDbContext.SaveChanges();
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return await _bookStoreDbContext.Genres.ToListAsync();
        }

        public async Task<Genre> GetGenreById(int id)
        {
            if (id == 0)
                throw new InvalidOperationException();
            var genre = await _bookStoreDbContext.Genres.FirstOrDefaultAsync(x => x.Id == id);
            if (genre is null)
                throw new ArgumentNullException();
            else
                return genre;
        }

        public async Task<Genre> UpdateGenre(Genre genre)
        {
            if (genre.Id == 0)
                throw new InvalidOperationException();
            var updatedGenre = await GetGenreById(genre.Id);
            if (updatedGenre is not null)
            {
                updatedGenre.Name = genre.Name;
                updatedGenre.IsActive = genre.IsActive;
            }
            else
                throw new ArgumentNullException();
            _bookStoreDbContext.Genres.Update(updatedGenre);
            _bookStoreDbContext.SaveChanges();
            return updatedGenre;
        }

        public bool IsAnyGenre(int id)
        {
            if (id == 0)
                throw new InvalidOperationException();
            return _bookStoreDbContext.Genres.Any(x => x.Id == id);

        }
    }
}
