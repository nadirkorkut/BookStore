using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.DeleteGenre;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputIsGiven_Validator_ShouldBeReturnErrors()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 0;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);       
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = genreId;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);   
        }

        [Fact]
        public void WhenValidInputIsGiven_Validator_ShouldNotBeReturnError()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(null);
            command.GenreId = 1;

            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count().Should().Be(0);
        }
    }
}