using RabbitMQ.Client;

namespace Core.Infrastructure
{
    public static class RabbitHelper
    {
        private static IConnection _conn;

        private static IModel _channel;

        public static IConnection RabbitConnection
        {
            get
            {
                if(_conn == null || !_conn.IsOpen) 
                {
                    ConnectionFactory factory = new ConnectionFactory();
                    factory.UserName = "admin";
                    factory.Password = "admin";
                    factory.VirtualHost = "/";
                    factory.HostName = "localhost";
                    _conn = factory.CreateConnection();
                }
                return _conn;
            }
        }

        public static IModel RabbitChannel
        {
            get
            {
                if(_channel == null || !_channel.IsOpen) 
                {
                    _channel = RabbitConnection.CreateModel();
                }
                return _channel;
            }
        }
    }    
}