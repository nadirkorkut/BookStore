using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;
using WebApi.Entities;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public CreateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.FirstName.ToLower() == Model.FirstName.ToLower() && x.LastName.ToLower() == Model.LastName.ToLower() && x.BirthDate.Date == Model.BirthDate.Date);

            if( author is not null)
                throw new InvalidOperationException("Yazar zaten mevcut");
            
            author = new Author();
            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.BirthDate = Model.BirthDate;
            
            _context.Authors.Add(author);
            _context.SaveChanges();
            
        }

        public class CreateAuthorModel
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public DateTime BirthDate { get; set; }
        }
    }
}