using RabbitMQ.Client;
using Core.Domain;
using System.Text.RegularExpressions;
using System.Text.Json;

namespace Core.Infrastructure
{
    public abstract class RabbitPublisher<T> where T : DomainEvent
    {
        private JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public void Publish(T payload)
        {
            var typeNames = Regex.Split(payload.GetType().Name, @"(?<!^)(?=[A-Z])");
            var exchangeName = $"TOPIC/{typeNames[0]}";
            var routeKey = $"{typeNames[0]}.{typeNames[1]}.{payload.Id}";
            var payloadBody = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload, _jsonSerializerOptions));
            RabbitHelper.RabbitChannel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false);
            RabbitHelper.RabbitChannel.BasicPublish(exchangeName, routeKey, null, payloadBody);
        }
    }
}