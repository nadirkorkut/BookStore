using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.GenreOperations.CreateGenre;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;

namespace Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IBookStoreDbContext _context;
        public CreateGenreCommandTests(CommonTestFixture classFixture)
        {
            _context = classFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context);
            command.Model = new CreateGenreModel(){ Name = genre.Name };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Türü Zaten Mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            CreateGenreCommand command = new CreateGenreCommand(_context);
            CreateGenreModel model = new CreateGenreModel() { Name = "Psychology" };
            command.Model = model;

            FluentActions.Invoking(() => command.Handle()).Invoke();

            var genre = _context.Genres.SingleOrDefault(genre => genre.Name == model.Name);

            genre.Should().NotBeNull();
            genre?.Name.Should().Be(model.Name);
        }
    }
}