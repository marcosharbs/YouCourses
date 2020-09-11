using Library.Domain.CourseAggregate.Repository;
using Library.Domain.AuthorAggregate.Repository;
using Core.Domain;

namespace Library.Domain
{
    public interface ILibraryUnitOfWork : IUnitOfWork
    {
        IAuthorRepository Authors { get; }
        ICourseRepository Courses { get; }
    }
}