using Xunit;
using Library.Data;
using Library.Domain.AuthorAggregate.Model;
using FluentAssertions;
using System;
using System.Linq;

namespace Library.Test.Data
{
    [Collection("Database")]
    public class AuthorRepositorySpec
    {
        [Fact]
        public void Valid_author_crud()
        {
           var appsettings = Config.InitConfiguration();

            var unitOfWork = new NHibernateUnitOfWork(SessionHelper.GetSessionFactory(appsettings.GetSection("Database")["ConnectionSrtring"]));

            unitOfWork.BeginUnit();
            
            var authors = unitOfWork.Authors;
            var id = Guid.NewGuid();
            var author = Author.Create(id, "Marcos Harbs", "http://marcos.com/image.jpeg");
            authors.AddOrUpdate(author);
            var authorDb = authors.GetById(id);
            authorDb.Id.Should().Be(author.Id);

            unitOfWork.CommitUnit();
            unitOfWork.BeginUnit();

            authors = unitOfWork.Authors;
            author = Author.Create(id, "Marcos Harbs Updated", "http://marcos.com/image.jpeg");
            authors.AddOrUpdate(author);
            authorDb = authors.GetById(id);
            authorDb.Id.Should().Be(author.Id);
            authorDb.AuthorName.Name.Should().Be(author.AuthorName.Name);

            var authorsCount = authors.Count();

            authorsCount.Should().Be(1);

            var allAuthors = authors.GetAll();
            var partialAuthors = authors.GetPartial(0, 20);

            allAuthors.Count.Should().Be(1);
            partialAuthors.Count.Should().Be(1);
            allAuthors.ToList()[0].Should().Be(authorDb);
            partialAuthors.ToList()[0].Should().Be(authorDb);

            unitOfWork.CommitUnit();
            unitOfWork.BeginUnit();

            authors = unitOfWork.Authors;
            authors.Remove(authorDb);
            
            unitOfWork.CommitUnit();

            unitOfWork.BeginUnit();

            authors = unitOfWork.Authors;

            authors.AddOrUpdate(Author.Create("Marcos Harbs Updated", "http://marcos.com/image.jpeg"));

            unitOfWork.RollbackUnit();
        }
    }
}