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
            var exchangeName = $"TOPIC/{typeNames[0].ToUpper()}";
            var routeKey = $"{typeNames[0].ToLower()}.{typeNames[1].ToLower()}.{payload.Id}";
            var payloadBody = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload, _jsonSerializerOptions));
            RabbitHelper.GetRabbitChannel(exchangeName).ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false);
            RabbitHelper.GetRabbitChannel(exchangeName).BasicPublish(exchangeName, routeKey, null, payloadBody);
        }
    }
}