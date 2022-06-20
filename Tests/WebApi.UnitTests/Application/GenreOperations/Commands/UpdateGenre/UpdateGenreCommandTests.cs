using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.UpdateGenre;
using WebApi.DBOperations;
using Xunit;
using static WebApi.Application.GenreOperations.UpdateGenre.UpdateGenreCommand;

namespace Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreCommandTest(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Theory]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void WhenNotExistedGenreIdIsGiven_InvalidOperationException_ShouldBeReturn(int genreId)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = genreId;
            command.Model = new UpdateGenreModel()
            {
                Name = "WhenNotExistedGenreIdIsGiven_InvalidOperationException_ShouldBeReturn"
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Kitap Türü Bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShoulBeUpdated()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            UpdateGenreModel model = new UpdateGenreModel()
            {
                Name = "WhenNotExistedGenreIdIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var updateGenre = _context.Genres.SingleOrDefault(x => x.Id == command.GenreId);
            updateGenre.Should().NotBeNull();
            updateGenre?.Name.Should().Be(model.Name);
        }
    }
}