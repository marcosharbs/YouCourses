using System;
using Core.Domain;

namespace User.Domain.UserAggregate.Event
{
    public class UserCreated : DomainEvent
    {
        public string Name { get; protected set; }
        public string Email { get; protected set; }
        public string ImageUrl { get; protected set; }

        private UserCreated(Guid id, string name, string email, string imageUrl) : base(id) {
            Id = id;
            Name = name;
            Email = email;
            ImageUrl = imageUrl;
        }

        public static UserCreated Create(Guid id, string name, string email, string imageUrl)
        {
            return new UserCreated(id, name, email, imageUrl);
        }
    }
}