// Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var queuName = "example-queue";
ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

Console.Write("Dinleneek topic formatını belirtiniz: ");
string topic = Console.ReadLine();
string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(queueName, "topic-exchange-example", topic);

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queueName, true, consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();