using FluentNHibernate.Cfg;
using Library.Data.Mapping;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace Library.Data
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
                                                m.FluentMappings.AddFromAssemblyOf<AuthorMap>();
                                                m.FluentMappings.AddFromAssemblyOf<VideoMap>();
                                                m.FluentMappings.AddFromAssemblyOf<CourseMap>();
                                            })
                                            .BuildSessionFactory();
            }
            return _sessionFactory;
        }

    }
}