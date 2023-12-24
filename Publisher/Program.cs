using System.Text;
using RabbitMQ.Client;

const string HOST_NAME = "localhost";
const string EXCHANGE_NAME = "RABBITMQ";

const string QUEUE_NAME = "lojinhaGabiroba";

const string QUEUE_KEY = "lojinhaGabirobaKey";

var factory = new ConnectionFactory
{
    HostName = HOST_NAME
};

using var connection = factory.CreateConnection(); // Obtendo a conexão

using var channel = connection.CreateModel(); // Criando o channel

channel.ExchangeDeclare(  // Criando um Exchange
    exchange: EXCHANGE_NAME,
    type: ExchangeType.Direct
);

var queue = channel.QueueDeclare( // Criando uma Queue
    queue: QUEUE_NAME,
    durable: false,
    autoDelete: false,
    arguments: null
); 

channel.QueueBind(  // Ligando a Queue na exchange
    exchange: EXCHANGE_NAME,
    queue: QUEUE_NAME,
    routingKey: QUEUE_KEY
);

channel.BasicPublish( // Publicando a mensagem na Queue lojinha.Gabiroba através da exchange lojinha.Gabiroba.Key
    exchange: EXCHANGE_NAME,
    routingKey: string.Empty,
    body: Encoding.UTF8.GetBytes("Hello World, I'm here!")
);

Console.WriteLine("Queue created with success");
