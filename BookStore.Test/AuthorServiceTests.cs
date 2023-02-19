using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Context;
using WebApi.Models.Entity;
using WebApi.Services;
using Xunit;


namespace BookStore.Test
{
    public class AuthorServiceTests : IClassFixture<CommonTextFixture>
    {
        private readonly BookStoreDbContext _context;

        public AuthorServiceTests(CommonTextFixture textFixture)
        {
            _context = textFixture.Context;
        }

        #region CreateAuthor

        [Fact]
        public void CreateAuthor_NameSurnameAvailable_ThrowInvalidOperationException()
        {
            //arrange
            var author = new Author() { Name = "Eymen", Surname = "Yacý", DateOfBirth = new DateTime(1997, 11, 22) };
            _context.Authors.Add(author);
            _context.SaveChanges();
            var author2 = new Author() { Name = "Eymen", Surname = "Yacý", DateOfBirth = new DateTime(1997, 11, 22) };

            AuthorService service = new AuthorService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.CreateAuthor(author2))
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author Already Available!");

        }

        [Fact]
        public async void CreateAuthor_WithValidAuthor_CreatesAuthor()
        {
            //arrange 
            var author = new Author() { Name = "Ali", Surname = "Feyzi", DateOfBirth = new DateTime(1997, 11, 22) };
            AuthorService service = new AuthorService(_context);
            //act
            await service.CreateAuthor(author);
            //Assert
            var createdAuthor = _context.Authors.FirstOrDefault(x => x.Name == author.Name && x.Surname == author.Surname && x.DateOfBirth == author.DateOfBirth);
            Assert.Equal(author.Name, createdAuthor.Name);
            Assert.Equal(author.Surname, createdAuthor.Surname);
            Assert.Equal(author.DateOfBirth, createdAuthor.DateOfBirth);
        }

        #endregion

        #region DeleteAuthor

        [Fact]
        public void DeleteAuthor_AuthorAvailable_ThrowArgumentNullException()
        {
            //arrange
            var author = new Author() { Name = "Eymen", Surname = "Yacý", DateOfBirth = new DateTime(1997, 11, 22) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            AuthorService service = new AuthorService(_context);
            //act & assert
            FluentActions
                .Invoking(() => service.DeleteAuthor(100))
                .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void DeleteAuthor_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange
            var author = new Author() { Name = "Eymen", Surname = "Yacý", DateOfBirth = new DateTime(1997, 11, 22) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            AuthorService service = new AuthorService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.DeleteAuthor(0))
                .Should().Throw<InvalidOperationException>();

        }
        [Fact]
        public async void DeleteAuthor_ShouldDeleteAuthor_WhenValidIdIsPassed()
        {
            //arrange
            var author = new Author() { Name = "Eymen", Surname = "Yacý", DateOfBirth = new DateTime(1997, 11, 22) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            AuthorService service = new AuthorService(_context);
            int id = _context.Authors.FirstOrDefault(x => x.Name == author.Name && x.Surname == author.Surname && x.DateOfBirth == author.DateOfBirth).Id;

            //act 

            await service.DeleteAuthor(id);
            var result = _context.Authors.FirstOrDefault(x => x.Id == id);

            //assert
            Assert.Equal(result, null);

        }

        #endregion

        #region GetAllAuthors
        [Fact]
        public async void GetAllAuthors_AuthorsAvailable_ReturnAuthors()
        {
            //arrange
            AuthorService service = new AuthorService(_context);

            //act
            var authors = await service.GetAllAuthors();

            //assert
            Assert.NotNull(authors);
            Assert.IsType<List<Author>>(authors);

        }

        #endregion

        #region GetAuthorById

        [Fact]
        public async void GetAuthorById_AvailableAuthor_ReturnAuthor()
        {
            //arrange
            var author = new Author() { Name = "Ali", Surname = "Duru", DateOfBirth = new DateTime(1997, 11, 22) };
            _context.Authors.Add(author);
            _context.SaveChanges();

            AuthorService service = new AuthorService(_context);

            //act

            var result = await service.GetAuthorById(author.Id);

            //assert

            Assert.Equal(author, result);

        }

        [Fact]
        public void GetAuthorById_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange

            AuthorService service = new AuthorService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.GetAuthorById(0))
                .Should().Throw<InvalidOperationException>();


        }

        [Fact]
        public async void GetAuthorById_InvalidIdRequest_ThrowArgumentNullException()
        {
            //arrange

            AuthorService service = new AuthorService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.GetAuthorById(100))
                .Should().Throw<ArgumentNullException>();

        }
        #endregion

        #region UpdateAuthor

        [Fact]
        public async void UpdateAuthor_IsAnyUpdateAuthor_ValidUpdate()
        {
            //arrange
            AuthorService service = new AuthorService(_context);
            var author = _context.Authors.FirstOrDefault(x => x.Name == "Gizem");
            if (author is not null)
            {
                author.Name = "Test";
                author.Surname = "Test";
                author.DateOfBirth = new DateTime(2000, 1, 1);
            }

            //act

            await service.UpdateAuthor(author);
            var updatedAuthor = _context.Authors.FirstOrDefault(x => x.Name == "Test" && x.Surname == "Test" && x.DateOfBirth == new DateTime(2000, 1, 1));

            //assert
            Assert.Equal(author.Name, updatedAuthor.Name);
            Assert.Equal(author.Surname, updatedAuthor.Surname);
            Assert.Equal(author.DateOfBirth, updatedAuthor.DateOfBirth);

        }

        [Fact]
        public async void UpdateAuthor_IsAnyUpdateAuthor_ThrowArgumentNullException()
        {
            //arrange
            AuthorService service = new AuthorService(_context);
            Author author = new Author();
            if (author is not null)
            {
                author.Id = 55;
                author.Name = "Test";
                author.Surname = "Test";
                author.DateOfBirth = new DateTime(2000, 1, 1);
            }

            //act & assert
            FluentActions
                .Invoking(() => service.UpdateAuthor(author))
                .Should().Throw<ArgumentNullException>();

        }

        #endregion

        #region IsAnyAuthor

        [Fact]
        public void IsAnyAuthor_IsThereAnAuthor_ReturnTrue()
        {
            //arrange 
            AuthorService service = new AuthorService(_context);

            //act 
            var result = service.IsAnyAuthor(1);

            //assert
            Assert.True(result);

        }

        [Fact]
        public void IsAnyAuthor_IsThereAnAuthor_ReturnFalse()
        {
            //arrange 
            AuthorService service = new AuthorService(_context);

            //act 
            var result = service.IsAnyAuthor(100);

            //assert
            Assert.False(result);

        }

        [Fact]
        public void IsAnyAuthor_ZeroIdRequest_ThrowInvalidOperationException()
        {
            //arrange 
            AuthorService service = new AuthorService(_context);

            //act & assert
            FluentActions
                .Invoking(() => service.IsAnyAuthor(0))
                .Should().Throw<InvalidOperationException>();

        }

        #endregion
    }
}
