using System;
using Core.Domain;

namespace Library.Domain.AuthorAggregate.Model
{
    public class Author : AggregateRoot
    {
        public virtual AuthorName AuthorName { get; protected set; }
        public virtual AuthorPicture AuthorPicture { get; protected set; }

        protected Author() {}

        private Author(Guid id, AuthorName authorName, AuthorPicture authorPicture) : base(id)
        {
            AuthorName = authorName;
            AuthorPicture = authorPicture;
        }

        public virtual void UpdateName(string name)
        {
            AuthorName = new AuthorName(name);
        }

        public virtual void UpdatePicture(string picture)
        {
            AuthorPicture = new AuthorPicture(picture);
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