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
    public class AuthorService : IAuthorService
    {
        private IBookStoreDbContext _bookStoreDbContext;

        public AuthorService(IBookStoreDbContext bookStoreDbContext)
        {
            _bookStoreDbContext = bookStoreDbContext;
        }

        public async Task<Author> CreateAuthor(Author author)
        {
            bool isAnyAuthor = _bookStoreDbContext.Authors.Any(x => x.Name == author.Name && x.Surname == author.Surname);
            if (isAnyAuthor is false)
                await _bookStoreDbContext.Authors.AddAsync(author);
            else
                throw new InvalidOperationException("Author Already Available!");
            _bookStoreDbContext.SaveChanges();
            return author;
        }

        public async Task DeleteAuthor(int id)
        {
            if (id == 0)
            {
                throw new InvalidOperationException("Id value cannot be zero");
            }
            var deletedAuthor = await GetAuthorById(id);
            if (deletedAuthor is not null)
                _bookStoreDbContext.Authors.Remove(deletedAuthor);
            else
                throw new ArgumentNullException("Author Already Available!");
            _bookStoreDbContext.SaveChanges();
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            if (_bookStoreDbContext.Authors is null)
                throw new ArgumentNullException("Not Found Authors");
            return await _bookStoreDbContext.Authors.ToListAsync();
        }

        public async Task<Author> GetAuthorById(int id)
        {
            if (id == 0)
                throw new InvalidOperationException("Id value cannot be zero");
            var author = await _bookStoreDbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            if (author is not null)
                await _bookStoreDbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            else throw new ArgumentNullException("Author Already Available!");

            return author;
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            var updatedAuthor = await GetAuthorById(author.Id);
            if (author.Id == 0)
                throw new InvalidOperationException("Id value cannot be zero");
            if (updatedAuthor is not null)
            {
                updatedAuthor.Name = author.Name;
                updatedAuthor.Surname = author.Surname;
                updatedAuthor.DateOfBirth = author.DateOfBirth;
            }
            else throw new ArgumentNullException("Author Already Available!");
            _bookStoreDbContext.Authors.Update(updatedAuthor);
            _bookStoreDbContext.SaveChanges();
            return updatedAuthor;
        }

        public bool IsAnyAuthor(int id)
        {
            if (id == 0)
                throw new InvalidOperationException("id not equal 0");

            return _bookStoreDbContext.Authors.Any(x => x.Id == id);
        }
    }
}
