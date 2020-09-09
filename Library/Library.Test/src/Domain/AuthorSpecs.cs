using System;
using Xunit;
using FluentAssertions;
using Library.Domain.AuthorAggregate.Model;

namespace Library.Test.Domain
{
    public class AuthorSpecs
    {
        [Fact]
        public void Two_authors_same_id_are_equals()
        {
            var id = Guid.NewGuid();

            var author1 = Author.Create(id, "Marcos Harbs", "http://marcosharbs.com");
            var author2 = Author.Create(id, "Joao Azevedo", "http://joaozevedo.com");
            
            author1.Should().Be(author2);
            author1.GetHashCode().Should().Be(author2.GetHashCode());
        }

        [Fact]
        public void Two_authors_different_id_are_not_equals()
        {
            var author1 = Author.Create(Guid.NewGuid(), "Marcos Harbs", "http://marcosharbs.com");
            var author2 = Author.Create(Guid.NewGuid(), "Joao Azevedo", "http://joaozevedo.com");
            
            author1.Should().NotBe(author2);
            author1.GetHashCode().Should().NotBe(author2.GetHashCode());
        }

        [Fact]
        public void Cannot_create_author_name_empty()
        {
            Action action = () => Author.Create("", "");
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void Two_author_name_equals_same_name()
        {
            var author1 = Author.Create(Guid.NewGuid(), "Marcos Harbs", "http://marcosharbs.com");
            var author2 = Author.Create(Guid.NewGuid(), "Marcos Harbs", "http://joaozevedo.com");

            author1.AuthorName.Should().Be(author2.AuthorName);
        }

        [Fact]
        public void Two_author_picture_equals_same_image()
        {
            var author1 = Author.Create(Guid.NewGuid(), "Marcos Harbs", "http://marcosharbs.com");
            var author2 = Author.Create(Guid.NewGuid(), "Marcos Harbs", "http://marcosharbs.com");

            author1.AuthorPicture.Should().Be(author2.AuthorPicture);
        }

    }
}
