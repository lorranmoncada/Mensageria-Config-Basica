using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello, World!");

var factory = new ConnectionFactory
{
    HostName = "localhost"
};

var connection = factory.CreateConnection();

using
var channel = connection.CreateModel();

//channel.QueueDeclare("product", exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    try
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        Console.WriteLine($"Product message received: {message}");

        // Confirma o recebimento da mensagem
        channel.BasicAck(eventArgs.DeliveryTag, false);
    }
    catch (Exception ex)
    {
        //não confirma o recebimento da mensagem e volta com ela para a fila
        channel.BasicNack(eventArgs.DeliveryTag, false, true);
    }

};

channel.BasicConsume(queue: "product", autoAck: false, consumer: consumer);
Console.ReadKey();

