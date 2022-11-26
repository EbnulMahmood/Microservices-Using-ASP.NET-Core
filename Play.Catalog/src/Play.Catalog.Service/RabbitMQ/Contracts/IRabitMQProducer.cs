namespace Play.Catalog.Service.RabbitMQ.Contracts
{
    public interface IRabitMQProducer
    {
        void SendItemMessage<T>(T message);
    }
}
