using System;

namespace Core.Domain
{
    public abstract class DomainEvent
    {
        public virtual Guid Id { get; protected set; }

        public DomainEvent(Guid id)
        {
            Id = id;
        }
    }
}