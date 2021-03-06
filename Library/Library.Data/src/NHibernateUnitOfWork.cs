using NHibernate;
using Library.Domain;
using Library.Domain.AuthorAggregate.Repository;
using Library.Domain.CourseAggregate.Repository;
using Library.Data.Repository;

namespace Library.Data
{
    public class NHibernateUnitOfWork : LibraryUnitOfWork
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public static NHibernateUnitOfWork Create(string databaseUrl)
        {
            var sessionFactory = SessionHelper.GetSessionFactory(databaseUrl);
            return new NHibernateUnitOfWork(sessionFactory);
        }

        override public IAuthorRepository GetAuthorRepository()
        {
            return new AuthorRepository(_session);
        }

        override public ICourseRepository GetCourseRepository()
        {
            return new CourseRepository(_session);
        }

        override protected void OnBeginUnit()
        {
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        override protected void OnCommitUnit()
        {
            _transaction.Commit();
            _session.Close();
        }
    }
}