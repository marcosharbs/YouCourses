using System.Collections.Generic;

namespace Core.Domain
{
    public abstract class UnitOfWork
    {
        private List<DomainEvent> _events = new List<DomainEvent>();

        public void AddEvent(DomainEvent domainEvent)
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