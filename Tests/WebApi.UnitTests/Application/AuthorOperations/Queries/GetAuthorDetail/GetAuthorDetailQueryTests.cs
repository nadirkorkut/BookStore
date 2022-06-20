using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
         private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 0;

            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunamadı");
        }

        [Fact]
        public void WhenExistAuthorIdIsGiven_AuthorDetail_ShouldBeReturn()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.AuthorId = 1;

            FluentActions.Invoking(() => query.Handle()).Invoke();

            var authorDetail = _context.Books.SingleOrDefault(authorDetail => authorDetail.Id == query.AuthorId);
            authorDetail.Should().NotBeNull();
        }
    }
}