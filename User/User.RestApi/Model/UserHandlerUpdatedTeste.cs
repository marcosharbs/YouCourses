using User.Domain.UserAggregate.Event;
using Core.Domain;
using System;

public class UserHandlerUpdatedTeste : IHandler<UserUpdated>
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