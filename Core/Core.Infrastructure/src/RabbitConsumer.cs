using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;
using System.Text.Json;

namespace Core.Infrastructure
{
    public abstract class RabbitConsumer<T>
    {
        private List<EventingBasicConsumer> _consumers = new List<EventingBasicConsumer>();

        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            AllowTrailingCommas = true,
            IgnoreNullValues = true,
            IgnoreReadOnlyProperties = true
        };

        public void init()
        {
            var exchangeName = $"TOPIC/{GetTopicName()}";
            var queueName = $"QUEUE/{GetQueueName()}";

            RabbitHelper.RabbitChannel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false);
            RabbitHelper.RabbitChannel.QueueDeclare(queueName, true, false, false, null);
            RabbitHelper.RabbitChannel.QueueBind(queueName, exchangeName, GetRoutingKey(), null);

            for(var i = 0; i < GetConsumersCount(); i++)
            {
                var consumer = new EventingBasicConsumer(RabbitHelper.RabbitChannel);
                consumer.Received += (ch, ea) =>
                {
                    var body = System.Text.Encoding.UTF8.GetString(ea.Body.ToArray());
                    T payload = JsonSerializer.Deserialize<T>(body, _jsonSerializerOptions);
                    Process(payload);
                    RabbitHelper.RabbitChannel.BasicAck(ea.DeliveryTag, false);
                };
                RabbitHelper.RabbitChannel.BasicConsume(GetQueueName(), false, consumer);
                _consumers.Add(consumer);
            }
        }

        protected abstract void Process(T payload);
        protected abstract string GetTopicName();
        protected abstract string GetQueueName();
        protected virtual int GetConsumersCount()
        {
            return 1;
        }
        protected virtual string GetRoutingKey()
        {
            return "#";
        }
    }
}