using System;
using Library.Domain.AuthorAggregate.Model;

namespace Library.RestApi.Model
{
    public class AuthorApi
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public AuthorApi() { }

        private AuthorApi(Guid id, string name, string imageUrl)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
        }

        public static AuthorApi FromDomain(Author author)
        {
            return new AuthorApi(author.Id, author.AuthorName.Name, author.AuthorPicture.ImageUrl);
        }

        public static Author ToDomain(AuthorApi author)
        {
            return Author.Create(author.Id, author.Name, author.ImageUrl);
        }
    }
}