using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;


namespace WebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if(context.Books.Any())
                {
                    return;
                }

                context.Books.AddRange(
                     new Book
                    {
                    //Id = 1,
                    Title = "Leon Startup",
                    GenreId = 1,
                    AuthorId = 1,
                    PageCount = 200,
                    publishDate = new DateTime(2001,06,12)
                    },
                    new Book
                    {
                    //Id = 2,
                    Title = "Herland",
                    GenreId = 2,
                    AuthorId = 2, 
                    PageCount = 250,
                    publishDate = new DateTime(2010,05,23)
                    },
                    new Book
                    {
                    //Id = 3,
                    Title = "Dune",
                    GenreId = 3,
                    AuthorId = 3, 
                    PageCount = 540,
                    publishDate = new DateTime(2002,12,21)
                    }
                );

                if(context.Genres.Any())
                {
                    return;
                }
                context.Genres.AddRange(
                    new Genre{
                        Name = "Personal Growth"
                    },
                    new Genre{
                        Name = "Science Fiction"
                    },
                    new Genre{
                        Name = "Romance"
                    }
                );
                
                if(context.Authors.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author{
                        FirstName = "Eric",
                        LastName = "Ries",
                        BirthDate = new DateTime(1987,09,22)
                    },
                    new Author{
                        FirstName = "Cherlotte Perkins",
                        LastName = "Gilmon",
                        BirthDate = new DateTime(1860,07,03)
                    },
                    new Author{
                        FirstName = "Frank",
                        LastName = "Herbert",
                        BirthDate = new DateTime(1920,10,08)
                    }
                );
                
                context.SaveChanges();
            }
        }
    }
}