using Library.Domain.AuthorAggregate.Model;
using Library.Domain.CourseAggregate.Model;
using Core.Domain;

namespace Library.Domain
{
    public interface ILibraryUnitOfWork : IUnitOfWork
    {
        IRepository<Author> Authors { get; }
        IRepository<Course> Courses { get; }
    }
}