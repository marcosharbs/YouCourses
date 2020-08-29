using Library.Domain.AuthorAggregate.Model;
using Library.Domain.CourseAggregate.Model;

namespace Library.Domain.Common
{
    public interface IUnitOfWork
    {
        IRepository<Author> Authors { get; }
        IRepository<Course> Courses { get; }
        void BeginUnit();
        void CommitUnit();
        void RollbackUnit();
    }
}