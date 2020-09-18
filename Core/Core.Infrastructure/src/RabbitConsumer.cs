using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public abstract class RabbitConsumer<T>
    {
        private List<AsyncEventingBasicConsumer> _consumers = new List<AsyncEventingBasicConsumer>();

        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            IgnoreReadOnlyProperties = true
        };

        public void init(string domain)
        {
            var exchangeName = GetTopicName();
            var queueName = GetQueueName(domain);

            for(var i = 0; i < GetConsumersCount(); i++)
            {
                var channelName = $"{queueName}_{Thread.CurrentThread.ManagedThreadId}+{i}";

                RabbitHelper.GetRabbitChannel(channelName).ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false);
                RabbitHelper.GetRabbitChannel(channelName).QueueDeclare(queueName, true, false, false, null);
                RabbitHelper.GetRabbitChannel(channelName).QueueBind(queueName, exchangeName, GetRoutingKey(), null);

                var consumer = new AsyncEventingBasicConsumer(RabbitHelper.GetRabbitChannel(channelName));
                consumer.Received += async (ch, ea) =>
                {
                    var body = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                    T payload = JsonSerializer.Deserialize<T>(body, _jsonSerializerOptions);
                    Process(payload);
                    RabbitHelper.GetRabbitChannel(channelName).BasicAck(ea.DeliveryTag, false);
                    await Task.Yield();
                };
                RabbitHelper.GetRabbitChannel(channelName).BasicConsume(queueName, false, consumer);
                _consumers.Add(consumer);
            }
        }

        protected abstract void Process(T payload);

        private string GetTopicName()
        {
            var typeNames = Regex.Split(this.GetType().Name, @"(?<!^)(?=[A-Z])");
            return $"TOPIC/{typeNames[0].ToUpper()}";
        }

        private string GetQueueName(string domain)
        {
            var typeNames = Regex.Split(this.GetType().Name, @"(?<!^)(?=[A-Z])");
            var queueName = "";
            for(var i = 0; i < typeNames.Length - 1; i++)
            {
                queueName += typeNames[i].ToUpper() + "_";
            }
            return $"QUEUE/{queueName}{domain.ToUpper()}";
        }

        private string GetRoutingKey()
        {
            var typeNames = Regex.Split(this.GetType().Name, @"(?<!^)(?=[A-Z])");
            var routingKey = "";
            for(var i = 0; i < typeNames.Length - 1; i++)
            {
                routingKey += typeNames[i].ToLower() + ".";
            }
            if(typeNames.Length > 2)
            {
                return $"{routingKey}*";
            }
            else
            {
                return $"{routingKey}*.*";
            }
        }

        protected virtual int GetConsumersCount()
        {
            return 1;
        }
    }
}