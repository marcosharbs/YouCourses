using System;

namespace Library.Domain.Common
{
    public abstract class AggregateRoot : Entity
    {

        public AggregateRoot(Guid id) : base(id) {}

        protected AggregateRoot() {}

    }
}