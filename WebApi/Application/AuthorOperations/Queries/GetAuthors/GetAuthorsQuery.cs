using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using WebApi.DBOperations;

namespace WebApi.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        public readonly IBookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<AuthorsViewModel> Handle()
        {
            var authors = _context.Authors.OrderBy(x => x.Id).ToList();
            List<AuthorsViewModel> returnObj = _mapper.Map<List<AuthorsViewModel>>(authors);
            return returnObj;
        }
    }

    public class AuthorsViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}