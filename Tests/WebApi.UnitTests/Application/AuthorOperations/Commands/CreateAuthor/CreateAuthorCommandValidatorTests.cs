using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(" "," ")]
        [InlineData(" ","K")]
        [InlineData(" ","Ko")]
        [InlineData("N"," ")]
        [InlineData("Na"," ")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName)
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel() 
            { 
                FirstName = firstName, 
                LastName = lastName, 
                BirthDate = DateTime.Now.AddYears(-10)
            };

            // act
            CreateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenBirthdateEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "Nadir",
                LastName = "Korkut",
                BirthDate = DateTime.Now.Date,
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null);
            command.Model = new CreateAuthorModel()
            {
                FirstName = "Nadir",
                LastName = "Korkut",
                BirthDate = DateTime.Now.Date.AddYears(-28)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}