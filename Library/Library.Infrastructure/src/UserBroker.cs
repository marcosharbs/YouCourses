using System;
using Library.Domain.AuthorAggregate.Model;

namespace Library.Infrastructure
{
    public class UserBroker
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public Author ToDomain()
        {
            return Author.Create(Id, Name, ImageUrl);
        }
    }
}