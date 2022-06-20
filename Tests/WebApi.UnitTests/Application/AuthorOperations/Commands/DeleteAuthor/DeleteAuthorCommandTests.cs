using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.DeleteAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Theory]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void WhenNotExistedAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = authorId;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar Bulunmadı!");
        }

        [Fact]
        public void WhenValidIdIsGiven_Author_ShouldBeDeleted()
        {
            var authorId = 1;

            _context.Books.RemoveRange(_context.Books.Where(x => x.AuthorId == authorId));
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context,_mapper);
            command.AuthorId = authorId;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedBook = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);
            deletedBook.Should().BeNull();
        }

         [Fact]
        public void WhenValidIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context, _mapper);
            command.AuthorId = 1;

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazarın yayınlanmış kitabı mevcut!");
        }
    }
}