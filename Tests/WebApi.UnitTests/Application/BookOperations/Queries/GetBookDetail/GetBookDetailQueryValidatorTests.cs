using FluentAssertions;
using TestSetup;
using WebApi.Application.BookOperations.Queries.GetBookDetail;
using Xunit;

namespace Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenInvalidBookIdIsGiven_Validator_ShouldBeReturnErrors(int bookId)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = bookId;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(null, null);
            query.BookId = 1;

            GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}