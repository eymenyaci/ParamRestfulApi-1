using BookStore.Api.Interfaces;
using BookStore.Test.TestSetup;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Context;
using WebApi.Models.Entity;
using WebApi.Services;
using Xunit;

namespace BookStore.Test
{
    public class GenreServiceTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;

        public GenreServiceTests(CommonTextFixture commonTextFixture)
        {
            _context = commonTextFixture.Context;
        }

        #region CreateGenre

        [Fact]
        public void CreateGenre_GenreNameAvailable_ThrowInvalidOperationException()
        {
            //arrange 
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            var genre2 = new Genre() { Id = 1, Name = "Test", IsActive = true };

            GenreService service = new GenreService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.CreateGenre(genre2))
                .Should().Throw<InvalidOperationException>();

        }

        [Fact]
        public async void CreateGenre_WithValidGenre_ReturnGenre()
        {
            //arrange 
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            GenreService service = new GenreService(_context);

            //act
            await service.CreateGenre(genre);
            var createdGenre = _context.Genres.First(x => x.Name == genre.Name);

            // assert
            Assert.Equal(genre.Id, createdGenre.Id);
            Assert.Equal(genre.Name, createdGenre.Name);
            Assert.Equal(genre.IsActive, createdGenre.IsActive);

        }


        #endregion

        #region DeleteGenre

        [Fact]
        public void DeleteGenre_GenreAvailable_ThrowArgumentNullException()
        {
            //arrange 
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            GenreService service = new GenreService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.DeleteGenre(100))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DeleteGenre_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange 
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GenreService service = new GenreService(_context);
            //act & assert
            FluentActions
                .Invoking(() => service.DeleteGenre(0))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async void DeleteGenre_ShouldDeleteGenre_WhenValidIdIsPassed()
        {
            //arrange 
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GenreService service = new GenreService(_context);
            int id = _context.Genres.FirstOrDefault(x => x.Name == genre.Name).Id;
            //act
            await service.DeleteGenre(id);
            bool result = _context.Genres.Any(x => x.Id == id && x.Name == genre.Name && x.IsActive == genre.IsActive);
            //assert
            Assert.False(result);

        }

        #endregion

        #region GetAllGenres

        [Fact]
        public async void GetAllGenres_GenresAvailable_ReturnGenres()
        {
            //arrange
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GenreService service = new GenreService(_context);

            //act 
            var genres = await service.GetAllGenres();


            //assert
            Assert.NotEqual(0, genres.Count());
            Assert.NotNull(genres);
            Assert.IsType<List<Genre>>(genres);
        }

        #endregion

        #region GetGenreById

        [Fact]
        public async void GetGenreById_AvailableGenre_ReturnGenre()
        {
            //arrange
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GenreService service = new GenreService(_context);

            //act
            var result = await service.GetGenreById(genre.Id);

            //assert
            Assert.Equal(result, genre);
        }

        [Fact]
        public void GetGenreById_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange
            GenreService service = new GenreService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.GetGenreById(0))
                .Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetGenreById_InvalidIdRequest_ThrowArgumentNullException()
        {
            //arrange

            GenreService service = new GenreService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.GetGenreById(100))
                .Should().Throw<ArgumentNullException>();

        }

        #endregion

        #region UpdateGenre

        [Fact]
        public async void UpdateGenre_IsAnyUpdateGenre_ValidUpdate()
        {
            //arrange
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            GenreService service = new GenreService(_context);

            var updatedGenre = _context.Genres.FirstOrDefault(x => x.Name == genre.Name);
            if (updatedGenre != null)
            {
                genre.IsActive = true;
                genre.Name = "Test";
            }

            //act

            await service.UpdateGenre(updatedGenre);
            var result = _context.Genres.FirstOrDefault(x => x.Name == updatedGenre.Name && x.IsActive == updatedGenre.IsActive);

            //assert
            Assert.Equal(result.Name, updatedGenre.Name);
            Assert.Equal(result.IsActive, updatedGenre.IsActive);

        }

        [Fact]
        public void UpdateGenre_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange
            var genre = new Genre() { Id = 0, Name = "Test", IsActive = true };
            GenreService service = new GenreService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.UpdateGenre(genre))
                .Should().Throw<InvalidOperationException>();
            
        }

        [Fact]
        public void UpdateGenre_IsAnyUpdateGenre_ThrowArgumentNullException()
        {
            //arrange
            GenreService service = new GenreService(_context);
            Genre genre = new Genre();
            genre.Id = 55;
            genre.Name = "Test";
            genre.IsActive = true;

            //act & assert
            FluentActions
                .Invoking(() => service.UpdateGenre(genre))
                .Should().Throw<ArgumentNullException>();
        }


        #endregion

        #region IsAnyGenre

        [Fact]
        public void IsAnyGenre_IsThereAnGenre_ReturnTrue()
        {
            //arrange
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GenreService service = new GenreService(_context);

            //act 
            var result = service.IsAnyGenre(genre.Id);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void IsAnyGenre_IsThereAnGenre_ReturnFalse()
        {
            //arrange
            var genre = new Genre() { Id = 1, Name = "Test", IsActive = true };
            _context.Genres.Add(genre);
            _context.SaveChanges();
            GenreService service = new GenreService(_context);

            //act 
            var result = service.IsAnyGenre(2);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void IsAnyGenre_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange
            GenreService service = new GenreService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.IsAnyGenre(0))
                .Should().Throw<InvalidOperationException>();
        }

        #endregion
    }
}
