namespace RabbitMqProdutorAPI.RabbitMq
{
    public interface IRabitMQProducer
    {
        public void SendProductMessage<T>(T message);
    }
}
