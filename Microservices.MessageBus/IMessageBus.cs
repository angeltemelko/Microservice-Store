namespace Microservices.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage<T>(T message, string topic_queue_name, string connectionString);
    }
}
