using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using WebApi.DBOperations;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void WhenNotExistedBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            // arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = bookId;

            // act & assert
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Silinecek Kitap Bulunamadı");
        }
       
        [Fact]
        public void WhenValidBookIdIsGiven_Book_ShouldBeDeleted()
        {
            //arrange
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 1;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var deleteBook = _context.Books.SingleOrDefault(x => x.Id == command.BookId);
            deleteBook.Should().BeNull();
        }
    }
}