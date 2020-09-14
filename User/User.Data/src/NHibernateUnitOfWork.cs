using NHibernate;
using User.Domain;
using User.Domain.UserAggregate.Repository;
using User.Data.Repository;

namespace User.Data
{
    public class NHibernateUnitOfWork : IUserUnitOfWork
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;

        public NHibernateUnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IUserRepository Users
        {
            get
            {
                return new UserRepository(_session);
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