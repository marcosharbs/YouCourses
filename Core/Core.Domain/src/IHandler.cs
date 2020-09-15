namespace Core.Domain
{
    public interface IHandler<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}