using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Queries.GetAuthorDetail;
using Xunit;

namespace Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
         [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenInvalidAuthorIdIsGiven_Validator_ShouldBeReturnErrors(int authorId)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = authorId;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidAuthorIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(null, null);
            query.AuthorId = 1;

            GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}