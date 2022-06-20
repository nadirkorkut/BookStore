using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 0;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }
        
        [Fact]
        public void WhenExistBookIdIsGiven_BookDetail_ShouldBeReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.BookId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var bookDetail = _context.Books.SingleOrDefault(bookDetail => bookDetail.Id == query.BookId);
            bookDetail.Should().NotBeNull();
        }
    }
}