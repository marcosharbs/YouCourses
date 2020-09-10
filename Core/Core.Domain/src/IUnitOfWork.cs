namespace Core.Domain
{
    public interface IUnitOfWork
    {
        void BeginUnit();
        void CommitUnit();
        void RollbackUnit();
    }
}