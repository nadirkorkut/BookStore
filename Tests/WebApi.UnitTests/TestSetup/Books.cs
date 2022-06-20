using System;
using WebApi.DBOperations;
using WebApi.Entities;

namespace TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
            context.Books.AddRange(
                new Book { Title = "Leon Startup", GenreId = 1, AuthorId = 1, PageCount = 200, publishDate = new DateTime(2001,06,12)},
                new Book { Title = "Herland", GenreId = 2, AuthorId = 2, PageCount = 250, publishDate = new DateTime(2010,05,23)},
                new Book { Title = "Dune", GenreId = 3, AuthorId = 3, PageCount = 540,publishDate = new DateTime(2002,12,21)}
            );
        }
    }
}