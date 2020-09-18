using RabbitMQ.Client;
using System.Collections.Generic;

namespace Core.Infrastructure
{
    public static class RabbitHelper
    {
        private static IConnection _conn;

        private static Dictionary<string, IModel> _channels = new Dictionary<string, IModel>();

        public static IConnection RabbitConnection
        {
            get
            {
                if(_conn == null || !_conn.IsOpen) 
                {
                    ConnectionFactory factory = new ConnectionFactory();
                    factory.DispatchConsumersAsync = true;
                    factory.UserName = "admin";
                    factory.Password = "admin";
                    factory.VirtualHost = "/";
                    factory.HostName = "localhost";
                    _conn = factory.CreateConnection();
                }
                return _conn;
            }
        }

        public static IModel GetRabbitChannel(string name)
        {
            if(_channels.ContainsKey(name) && _channels[name].IsOpen)
            {
                return _channels[name];
            }
            _channels[name] = RabbitConnection.CreateModel();
            return _channels[name];
        }
    }    
}