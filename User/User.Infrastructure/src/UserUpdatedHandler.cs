using Core.Domain;
using User.Domain.UserAggregate.Event;
using System;

namespace User.Infrastructure
{
    public class UserUpdatedHandler : IHandler<UserUpdated>
    {
        public void Handle(UserUpdated domainEvent)
        {
            Console.WriteLine("usuario atualizado");
            Console.WriteLine(domainEvent.Id);
            Console.WriteLine(domainEvent.Email);
            Console.WriteLine(domainEvent.Name);
            Console.WriteLine(domainEvent.ImageUrl);
        }
    }
}