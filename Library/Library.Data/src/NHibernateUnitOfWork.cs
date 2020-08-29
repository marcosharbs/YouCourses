using NHibernate;
using FluentNHibernate.Cfg;
using Library.Data.Mapping;
using FluentNHibernate.Cfg.Db;
using Library.Domain.Common;
using Library.Domain.AuthorAggregate.Model;
using Library.Domain.CourseAggregate.Model;
using Library.Data.Repository;

namespace Library.Data
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private ISessionFactory _sessionFactory;
        private ISession _session;
        private ITransaction _transaction;
        private ISessionFactory SessionFactory {
            get
            {
                if(_sessionFactory == null)
                {
                    var appsettings = Config.InitConfiguration();

                    _sessionFactory = Fluently.Configure()
                                                .Database(PostgreSQLConfiguration.PostgreSQL82
                                                .ConnectionString(c => c.Is(appsettings.GetSection("Database")["ConnectionSrtring"])))
                                                .Mappings(m => {
                                                    m.FluentMappings.AddFromAssemblyOf<AuthorMap>();
                                                    m.FluentMappings.AddFromAssemblyOf<VideoMap>();
                                                    m.FluentMappings.AddFromAssemblyOf<CourseMap>();
                                                })
                                                .BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public IRepository<Author> Authors
        {
            get
            {
                return new AuthorRepository(_session);
            }
        }

        public IRepository<Course> Courses
        {
            get
            {
                return new CourseRepository(_session);
            }
        }

        public void BeginUnit()
        {
            _session = SessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        public void CommitUnit()
        {
            _transaction.Commit();
            _session.Close();
        }

        public void RollbackUnit()
        {
            _transaction.Rollback();
            _session.Close();
        }
    }
}