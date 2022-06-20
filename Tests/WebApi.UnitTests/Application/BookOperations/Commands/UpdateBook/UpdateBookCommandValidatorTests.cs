using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.UpdateBook;
using Xunit;
using static WebApi.Application.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"",0,0)]
        [InlineData(0,"Boo",0,0)]
        [InlineData(0,"Book",0,0)]
        [InlineData(0,"Book",0,1)]
        [InlineData(0,"Book",1,0)]
        [InlineData(0,"Book",1,1)]
        [InlineData(1,"",0,0)]
        [InlineData(1,"Boo",0,0)]
        [InlineData(1,"Book",0,0)]
        [InlineData(1,"Book",0,1)]
        [InlineData(1,"Book",1,0)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId, string title, int genreId, int authorId)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = bookId;
            command.Model = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                AuthorId = authorId
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.BookId = 1;
            command.Model = new UpdateBookModel()
            {
                Title = "WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError",
                GenreId = 3,
                AuthorId = 2
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}