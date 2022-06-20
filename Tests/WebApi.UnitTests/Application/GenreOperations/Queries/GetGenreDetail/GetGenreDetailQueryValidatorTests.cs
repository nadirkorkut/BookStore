using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.Queries.GetGenreDetail;
using Xunit;

namespace Application.GenreOperations.Queries.GetBookDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(null)]
        public void WhenInvalidGenreIdIsGiven_Validator_ShouldBeReturnErrors(int genreId)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = genreId;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var results = validator.Validate(query);

            results.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidBookIdIsGiven_Validator_ShouldNotBeReturnError()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(null, null);
            query.GenreId = 1;

            GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
            var result = validator.Validate(query);

            result.Errors.Count.Should().Be(0);
        }
    }
}