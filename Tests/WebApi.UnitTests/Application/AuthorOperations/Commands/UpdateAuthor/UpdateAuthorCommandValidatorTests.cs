using System;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"","","2022-06-21")]
        [InlineData(0,"A","B","2022-06-21")]
        [InlineData(0,"A","BB","2022-06-21")]
        [InlineData(0,"AA","B","2022-06-21")]
        [InlineData(0,"","B","2022-06-21")]
        [InlineData(0,"A","","2022-06-21")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string firstName, string lastName, string birthDate)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = DateTime.Parse(birthDate)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "Nadir",
                LastName = "Korkut",
                BirthDate = DateTime.Now.AddYears(-14)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public void WhenDateTimeDateEqualsNowIsGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.AuthorId = 1;
            command.Model = new UpdateAuthorModel() 
            { 
                FirstName = "Nadir", 
                LastName = "Korkut", 
                BirthDate = DateTime.Now
            };

            UpdateAuthorCommandValidator validator = new();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}