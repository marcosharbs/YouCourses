using User.Domain.UserAggregate.Event;
using Core.Domain;
using System;

public class UserHandlerTeste : IHandler<UserCreated>
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