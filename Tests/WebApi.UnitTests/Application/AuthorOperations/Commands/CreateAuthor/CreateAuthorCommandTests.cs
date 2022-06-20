using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.CreateAuthor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using static WebApi.Application.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange (Hazirlik)
            var author = new Author(){FirstName = "Nadir", LastName = "Korkut", BirthDate = new DateTime(1994, 01, 05)};
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            command.Model = new CreateAuthorModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };

            //act & assert  (Calistirma - Dogrulama)
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yazar zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context);
            CreateAuthorModel model = new CreateAuthorModel(){ FirstName = "Nadir", LastName = "Korkut", BirthDate = DateTime.Now.Date.AddYears(-10)};
            command.Model = model;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert
            var author = _context.Authors.SingleOrDefault(author => author.FirstName == model.FirstName);

            author.Should().NotBeNull();
            author?.FirstName.Should().Be(model.FirstName);
            author?.LastName.Should().Be(model.LastName);
            author?.BirthDate.Should().Be(model.BirthDate);

        }
    }
}