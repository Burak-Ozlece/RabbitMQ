// Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var queuName = "example-queue";
ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

string queueName = "example-p2p-queue";

channel.QueueDeclare(queueName, exclusive: false, autoDelete: false);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queueName,autoAck:false,consumer:consumer);

consumer.Received += (sender, e) =>
{
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();