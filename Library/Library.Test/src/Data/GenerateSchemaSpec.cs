using Xunit;
using FluentNHibernate.Cfg;
using Library.Data.Mapping;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Cfg.Db;
using System;
using System.IO;

namespace Library.Test.Data
{
    public class GenerateSchemaSpec
    {
        [Fact]
        public void Can_generate_schema()
        {
            var appsettings = Config.InitConfiguration();

            Action<string> updateExport = x =>
            {
                using (var file = new FileStream("../../../db.sql", FileMode.Append, FileAccess.Write))
                using (var sw = new StreamWriter(file))
                {
                    sw.Write(x);
                }
            };

            Fluently.Configure()
                    .Database(PostgreSQLConfiguration.PostgreSQL82
                    .ConnectionString(c => c.Is(appsettings.GetSection("Database")["ConnectionSrtring"])))
                    .Mappings(m => {
                        m.FluentMappings.AddFromAssemblyOf<AuthorMap>();
                        m.FluentMappings.AddFromAssemblyOf<VideoMap>();
                        m.FluentMappings.AddFromAssemblyOf<CourseMap>();
                    })
                    .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(updateExport, false))
                    .BuildSessionFactory();
            
        }
    }
}