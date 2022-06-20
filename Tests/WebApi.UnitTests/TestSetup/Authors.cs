using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { FirstName = "Eric", LastName = "Ries", BirthDate = new DateTime(1987,09,22) },
                new Author { FirstName = "Cherlotte Perkins",  LastName = "Gilmon", BirthDate = new DateTime(1860,07,03) },
                new Author { FirstName = "Frank", LastName = "Herbert", BirthDate = new DateTime(1920,10,08) }
            );
        }
    }
}