using System.Runtime.InteropServices;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer;

public delegate void ShowMessage(string message);
public class ConsumerClass 
{
    public void ReceiveAsync(ShowMessage cb)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };

        using var connection = factory.CreateConnection();

        using var channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: "gabriel_queue",
            durable: false,
            exclusive: false,
            autoDelete: false
        );


        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) => 
        {
            var body = ea.Body.ToArray();
            cb(Encoding.UTF8.GetString(body));
        };

        channel.BasicConsume(
            queue: "gabriel_queue",
            autoAck: true,
            consumer: consumer
        );
    }
}