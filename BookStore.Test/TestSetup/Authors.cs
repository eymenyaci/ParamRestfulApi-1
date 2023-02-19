using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models.Context;
using WebApi.Models.Entity;

namespace BookStore.Test.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { Name = "Gizem",Surname = "Kılıç",DateOfBirth= new DateTime(1998,02,27)},
                new Author { Name = "Eymen",Surname = "Yacı",DateOfBirth= new DateTime(1997,11,22)}
                );
        }
    }
}
