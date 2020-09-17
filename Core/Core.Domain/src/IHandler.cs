namespace Core.Domain
{
    public interface IHandler<T> where T : DomainEvent
    {
        void Handle(T domainEvent);
    }
}