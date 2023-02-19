using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace BookStore.Test.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                new Genre { Name = "History", IsActive = true },
                new Genre { Name = "Math", IsActive = true }
                );
        }
    }
}
