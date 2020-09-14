using FluentNHibernate.Cfg;
using User.Data.Mapping;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace User.Data
{
    public static class SessionHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory GetSessionFactory(string databaseConnection)
        {
            if(_sessionFactory == null)
            {
                 _sessionFactory = Fluently.Configure()
                                            .Database(PostgreSQLConfiguration.PostgreSQL82
                                            .ConnectionString(c => c.Is(databaseConnection)))
                                            .Mappings(m => {
                                                m.FluentMappings.AddFromAssemblyOf<UserMap>();
                                            })
                                            .BuildSessionFactory();
            }
            return _sessionFactory;
        }

    }
}