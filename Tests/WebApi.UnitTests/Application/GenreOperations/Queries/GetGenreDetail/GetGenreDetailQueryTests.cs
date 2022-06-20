using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Queries.GetBookDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 0;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı");
        }

       [Fact]
        public void WhenExistGenreIdIsGiven_GenreDetail_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.GenreId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var genreDetail = _context.Books.SingleOrDefault(genreDetail => genreDetail.Id == query.GenreId);
            genreDetail.Should().NotBeNull();
        }
    }
}