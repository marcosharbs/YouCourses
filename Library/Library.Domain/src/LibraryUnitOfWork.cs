using Library.Domain.CourseAggregate.Repository;
using Library.Domain.AuthorAggregate.Repository;
using Core.Domain;

namespace Library.Domain
{
    public abstract class LibraryUnitOfWork : UnitOfWork
    {
        public abstract IAuthorRepository GetAuthorRepository();
        public abstract ICourseRepository GetCourseRepository();
    }
}