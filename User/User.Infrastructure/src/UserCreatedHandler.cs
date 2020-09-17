using Core.Domain;
using User.Domain.UserAggregate.Event;
using Core.Infrastructure;

namespace User.Infrastructure
{
    public class UserCreatedHandler : RabbitPublisher<UserCreated>, IHandler<UserCreated>
    {
        public void Handle(UserCreated domainEvent)
        {
            Publish(domainEvent);
        }
    }
}