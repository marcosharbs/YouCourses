using NHibernate;
using Library.Domain;
using Library.Domain.AuthorAggregate.Repository;
using Library.Domain.CourseAggregate.Repository;
using Library.Data.Repository;

namespace Library.Data
{
    public class NHibernateUnitOfWork : ILibraryUnitOfWork
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IAuthorRepository Authors
        {
            get
            {
                return new AuthorRepository(_session);
            }
        }

        public ICourseRepository Courses
        {
            get
            {
                return new CourseRepository(_session);
            }
        }

        public void BeginUnit()
        {
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        public void CommitUnit()
        {
            _transaction.Commit();
            _session.Close();
        }

        public void RollbackUnit()
        {
            if(_transaction != null)
                 _transaction.Rollback();

            if(_session != null)
                _session.Close();
        }
    }
}