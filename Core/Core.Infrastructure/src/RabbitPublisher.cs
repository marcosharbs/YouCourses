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
            var classNames = Regex.Split(payload.GetType().Name, @"(?<!^)(?=[A-Z])");
            var exchangeName = $"TOPIC/{classNames[0].ToUpper()}";
            var routeKey = $"{classNames[0].ToLower()}.{classNames[1].ToLower()}.{payload.Id}";
            var payloadBody = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(payload, _jsonSerializerOptions));
            RabbitHelper.GetRabbitChannel(exchangeName).ExchangeDeclare(exchangeName, ExchangeType.Topic, true, false);
            RabbitHelper.GetRabbitChannel(exchangeName).BasicPublish(exchangeName, routeKey, null, payloadBody);
        }
    }
}