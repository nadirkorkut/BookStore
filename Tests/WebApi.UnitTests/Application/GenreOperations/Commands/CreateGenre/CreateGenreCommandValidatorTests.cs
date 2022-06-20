using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData("G")]
        [InlineData(null)]
        [InlineData("Gen")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string name)
        {

            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            {
                Name = name
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateGenreCommand command = new CreateGenreCommand(null);
            command.Model = new CreateGenreModel()
            {
                Name = "WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError"
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}