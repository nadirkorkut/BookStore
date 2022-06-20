using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Commands.DeleteBook;
using Xunit;

namespace Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 0;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = bookId;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var results = validator.Validate(command);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteBookCommand command = new DeleteBookCommand(null);
            command.BookId = 1;

            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }   
    }
}