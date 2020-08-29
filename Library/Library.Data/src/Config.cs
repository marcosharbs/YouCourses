using Microsoft.Extensions.Configuration;
using System.IO;

namespace Library.Data
{
    public static class Config
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile(Path.GetFullPath("../../../appsettings.json"))
                .Build();
                return config;
        }
    }
}