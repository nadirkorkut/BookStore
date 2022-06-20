using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;

        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenNotExistedBookIdIsGiven_InvalidOperationException_ShouldBeReturn(int bookId)
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = "WhenNotExistedBookIdIsGiven_InvalidOperationException_ShouldBeReturn",
                GenreId = 2,
                AuthorId = 1
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Bulunamadı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShoulBeUpdated()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            UpdateBookModel model = new UpdateBookModel()
            {
                Title = "Book",
                GenreId = 2,
                AuthorId = 2
            };
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var updateBook = _context.Books.SingleOrDefault(x => x.Id == command.BookId);

            updateBook.Should().NotBeNull();
            updateBook?.Title.Should().Be(model.Title);
            updateBook?.GenreId.Should().Be(model.GenreId);
            updateBook?.AuthorId.Should().Be(model.AuthorId);
        }
    }
}