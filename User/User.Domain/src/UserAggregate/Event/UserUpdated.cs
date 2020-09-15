using System;
using Core.Domain;

namespace User.Domain.UserAggregate.Event
{
    public class UserUpdated : IDomainEvent
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string ImageUrl { get; protected set; }

        private UserUpdated(Guid id, string name, string email, string imageUrl) {
            Id = id;
            Name = name;
            Email = email;
            ImageUrl = imageUrl;
        }

        public static UserUpdated Create(Guid id, string name, string email, string imageUrl)
        {
            return new UserUpdated(id, name, email, imageUrl);
        }
    }
}