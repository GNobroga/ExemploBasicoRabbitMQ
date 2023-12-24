using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace Publisher;

public class PublisherClass
{
    public void Send(string message)
    {   
        // Configurando a fabrica de conexões (By: Gabriel)
        var factory = new ConnectionFactory 
        {
            HostName = "localhost"
        };

        // Estabelecendo uma conexão
        using var connection = factory.CreateConnection();

        // Criando um canal
        using var channel = connection.CreateModel();


        channel.QueueDeclare(
            queue: "gabriel_queue",
            durable: false, // Se for true persiste a fila
            exclusive: false, // Se for true indica que é exclusiva a essa conexão
            autoDelete: false, // Se for true será deletada após o seu consumo
            arguments: null
        );

        string json = JsonSerializer.Serialize(new {
            Name = "Gabriel",
            Body = message
        });

        channel.BasicPublish(
            exchange: "",
            routingKey: "gabriel_queue",
            basicProperties: null,
            body: Encoding.UTF8.GetBytes(json)
        );
    }
}
