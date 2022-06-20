using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.UpdateGenre;
using Xunit;
using static WebApi.Application.GenreOperations.UpdateGenre.UpdateGenreCommand;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, " ")]
        [InlineData(0, "N")]
        [InlineData(0, "Na")]
        [InlineData(0, "Nam")]
        [InlineData(1, " ")]
        [InlineData(1, "N")]
        [InlineData(1, "Na")]
        [InlineData(1, "Nam")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId, string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = name
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.GenreId = 1;
            command.Model = new UpdateGenreModel()
            {
                Name = "WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError"
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}