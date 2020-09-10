using System;
using Core.Domain;

namespace Library.Domain.AuthorAggregate.Model
{
    public class Author : AggregateRoot
    {
        public virtual AuthorName AuthorName { get; }
        public virtual AuthorPicture AuthorPicture { get; }

        protected Author() {}

        private Author(Guid id, AuthorName authorName, AuthorPicture authorPicture) : base(id)
        {
            AuthorName = authorName;
            AuthorPicture = authorPicture;
        }

        public static Author Create(Guid id, string name, string imageUrl)
        {
            return new Author(id, new AuthorName(name), new AuthorPicture(imageUrl));
        }

        public static Author Create(string name, string imageUrl)
        {
            return Create(Guid.NewGuid(), name, imageUrl);
        }
    }
}