using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace RabbitMqProdutorAPI.RabbitMq
{

    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendProductMessage<T>(T message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
            };

            var connection = factory.CreateConnection();

            using
            // cada publicador deve ter seu channel
            var channel = connection.CreateModel();

            //Para caso eu n tenha minha fila criada
            //channel.QueueDeclare("product", exclusive: false);

            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "product.teste", routingKey: "product", body: body);

        }
    }
}
