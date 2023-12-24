using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

const string HOST_NAME = "localhost";
const string EXCHANGE_NAME = "rabbit_mq";

const string QUEUE_NAME = "lojinha.Gabiroba";

const string QUEUE_KEY = "lojinha.Gabiroba.Key";

var factory = new ConnectionFactory
{
    HostName = HOST_NAME
};

using var connection = factory.CreateConnection(); // Obtendo a conexão

var channel = connection.CreateModel();

channel.QueueBind(exchange: EXCHANGE_NAME, queue: QUEUE_NAME, routingKey: QUEUE_KEY);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, arg) => 
{
    Console.WriteLine(Encoding.UTF8.GetString(arg.Body.Span));
};


channel.BasicConsume(
    queue: QUEUE_NAME,
    autoAck: true,
    consumer: consumer
);