using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private const string connectionString = "Endpoint=sb://microserivce-servicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=pqLQT6ynmKT31hNPbksOJxGu1shGf+fJ1+ASbCfhBxU=";

        public async Task PublishMessage<T>(T message, string topic_queue_name, string connectionString)
        {
            await using ServiceBusClient client = new(connectionString);

            ServiceBusSender sender = client.CreateSender(topic_queue_name);

            string jsonMessage = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalMessage = new(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();
        }
    }
}
