using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        
        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Güncellenecek Yazar Bulunamadı!");
            
            author.FirstName = string.IsNullOrEmpty(Model.FirstName.Trim()) ? author.FirstName : Model.LastName;
            author.LastName = string.IsNullOrEmpty(Model.LastName.Trim()) ? author.LastName : Model.LastName;
            author.BirthDate = Model.BirthDate != default ? author.BirthDate : Model.BirthDate;

            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}