using System;
using System.Linq;
using FluentAssertions;
using TestSetup;
using WebApi.Application.AuthorOperations.Commands.UpdateAuthor;
using WebApi.DBOperations;
using Xunit;

namespace Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        
        [Theory]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        public void WhenNotExistedAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn(int authorId)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = authorId;
            command.Model = new UpdateAuthorModel()
            {
                FirstName = "Update Author FirstName",
                LastName = "Update Author LastName",
                BirthDate = DateTime.Now.AddYears(-15)
            };

            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Güncellenecek Yazar Bulunamadı!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShoulBeUpdated()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;
            UpdateAuthorModel model = new UpdateAuthorModel()
            {
                FirstName = "Nadir",
                LastName = "Korkut",
                BirthDate = DateTime.Now.AddYears(-10)
            };
            command.Model = model;

            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            var updateAuthor = _context.Authors.SingleOrDefault(x => x.Id == command.AuthorId);

            updateAuthor.Should().NotBeNull();
            updateAuthor?.FirstName.Should().NotBeNull(model.FirstName);
            updateAuthor?.LastName.Should().NotBeNull(model.LastName);
            updateAuthor?.BirthDate.Should().NotBe(model.BirthDate);
        }
    }
}