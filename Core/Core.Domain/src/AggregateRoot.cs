using System;

namespace Core.Domain
{
    public abstract class AggregateRoot : Entity
    {

        public AggregateRoot(Guid id) : base(id) {}

        protected AggregateRoot() {}

    }
}