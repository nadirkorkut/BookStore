using System;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public DeleteAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(x => x.AuthorId == AuthorId);
            if(book is not null)
                throw new InvalidOperationException("Yazarın yayınlanmış kitabı mevcut!");

            var author = _context.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if(author is null)
                throw new InvalidOperationException("Yazar Bulunmadı!");
            
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}