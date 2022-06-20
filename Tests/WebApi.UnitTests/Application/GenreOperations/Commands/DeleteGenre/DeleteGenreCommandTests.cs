using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.DeleteGenre;
using WebApi.DBOperations;
using Xunit;

namespace Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void WhenNotExistedGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = genreId;

             FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Bulunamadı!");
        }
        [Fact]
        public void WhenValidGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var deletedGenre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            deletedGenre.Should().BeNull();
        }
    }
}