using Xunit;
using Library.Application;
using Moq;
using FluentAssertions;
using Library.Domain.AuthorAggregate.Repository;
using Library.Domain;
using Library.Domain.AuthorAggregate.Model;
using System;

namespace Library.Test.Data
{
    public class UpdateAuthorUseCaseSpec
    {
        [Fact]
        public void Should_update_author()
        {
            var authorId = Guid.NewGuid();

            var author = Author.Create(authorId, "Marcos Harbs Updated", "http://marcos.harbs.com/imagem_01.jpeg");
            
            var authorDB = Author.Create(authorId, "Marcos Harbs", "http://marcos.harbs.com/imagem.jpeg");

            var mockAuthorRepository = new Mock<IAuthorRepository>();
            mockAuthorRepository.Setup(mock => mock.GetById(author.Id)).Returns(authorDB);
            mockAuthorRepository.Setup(mock => mock.AddOrUpdate(authorDB));

            var mockUnitOfWork = new Mock<LibraryUnitOfWork>();
            mockUnitOfWork.Setup(mock => mock.GetAuthorRepository()).Returns(mockAuthorRepository.Object);

            var useCase = new UpdateAuthorUseCase(author, mockUnitOfWork.Object);
            var updatedAuthor = useCase.Execute();

            mockAuthorRepository.Verify(mock => mock.GetById(author.Id), Times.Exactly(1));
            mockAuthorRepository.Verify(mock => mock.AddOrUpdate(author), Times.Exactly(1));
            updatedAuthor.Should().Be(author);
        }
    }
}