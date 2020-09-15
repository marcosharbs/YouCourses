using Core.Domain;
using User.Domain.UserAggregate.Event;
using System;

namespace User.Infrastructure
{
    public class UserCreatedHandler : IHandler<UserCreated>
    {
        public void Handle(UserCreated domainEvent)
        {
            Console.WriteLine("usuario criado");
            Console.WriteLine(domainEvent.Id);
            Console.WriteLine(domainEvent.Email);
            Console.WriteLine(domainEvent.Name);
            Console.WriteLine(domainEvent.ImageUrl);
        }
    }
}