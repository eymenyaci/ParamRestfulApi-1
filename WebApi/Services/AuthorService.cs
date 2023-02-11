using Microsoft.EntityFrameworkCore;
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
        public async Task<Author> CreateAuthor(Author author)
        {
            using (var myDbContext = new MyDbContext())
            {
                bool isAnyAuthor = myDbContext.Authors.Any(x => x.Id == author.Id);
                if (isAnyAuthor is false)
                    await myDbContext.Authors.AddAsync(author);
                await myDbContext.SaveChangesAsync();
                return author;
            }
        }

        public async Task DeleteAuthor(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                var deletedAuthor = await GetAuthorById(id);
                if (deletedAuthor is not null)
                    myDbContext.Authors.Remove(deletedAuthor);
                await myDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Author>> GetAllAuthors()
        {
            using (var myDbContext = new MyDbContext())
            {
                return await myDbContext.Authors.ToListAsync();
            }
        }

        public async Task<Author> GetAuthorById(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                return await myDbContext.Authors.FirstOrDefaultAsync(x => x.Id == id);
            }
        }

        public async Task<Author> UpdateAuthor(Author author)
        {
            using (var myDbContext = new MyDbContext())
            {
                var updatedAuthor = await GetAuthorById(author.Id);
                if (updatedAuthor is not null)
                {
                    updatedAuthor.Name = author.Name;
                    updatedAuthor.Surname = author.Surname;
                    updatedAuthor.DateOfBirth = author.DateOfBirth;
                }
                myDbContext.Authors.Update(updatedAuthor);
                await myDbContext.SaveChangesAsync();
                return updatedAuthor;
            }
        }

        public bool IsAnyAuthor(int id)
        {
            using (var myDbContext = new MyDbContext())
            {
                return myDbContext.Authors.Any(x => x.Id == id);
            }
        }
    }
}
