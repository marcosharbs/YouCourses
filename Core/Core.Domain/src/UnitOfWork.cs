using System.Collections.Generic;

namespace Core.Domain
{
    public abstract class UnitOfWork
    {
        private List<IDomainEvent> _events = new List<IDomainEvent>();

        public void AddEvent(IDomainEvent domainEvent)
        {
            _events.Add(domainEvent);
        }

        public void BeginUnit()
        {
            OnBeginUnit();
        }
        
        public void CommitUnit()
        {
            OnCommitUnit();
            
            foreach (var domainEvent in _events)
            {
                DomainHandlers.DispatchEvent(domainEvent);
            }

            _events.Clear();
        }
        
        public void RollbackUnit()
        {
            OnRollbackUnit();
            _events.Clear();
        }
        
        protected abstract void OnBeginUnit();
        protected abstract void OnCommitUnit();
        protected abstract void OnRollbackUnit();
    }
}