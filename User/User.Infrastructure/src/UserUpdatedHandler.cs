using Core.Domain;
using User.Domain.UserAggregate.Event;
using Core.Infrastructure;

namespace User.Infrastructure
{
    public class UserUpdatedHandler : RabbitPublisher<UserUpdated>, IHandler<UserUpdated>
    {
        public void Handle(UserUpdated domainEvent)
        {
            Publish(domainEvent);
        }
    }
}