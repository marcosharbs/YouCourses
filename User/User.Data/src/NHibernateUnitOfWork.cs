using NHibernate;
using User.Domain;
using User.Domain.UserAggregate.Repository;
using User.Data.Repository;

namespace User.Data
{
    public class NHibernateUnitOfWork : UserUnitOfWork
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        override public IUserRepository GetUserRepository()
        {
            return new UserRepository(_session);
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

        override protected void OnRollbackUnit()
        {
            if(_transaction != null)
                 _transaction.Rollback();

            if(_session != null)
                _session.Close();
        }
    }
}