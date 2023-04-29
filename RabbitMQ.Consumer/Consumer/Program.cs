using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
var exchangeName = "direct-exchange-example";
var routingKeyName = "direct-queue-example";
ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();
// 1. Adım
channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

// 2. Adım
string queueName = channel.QueueDeclare().QueueName;

// 3. Adım
channel.QueueBind(queue:queueName, exchange:exchangeName, routingKey:routingKeyName);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);
consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();

// 1. Adım : Publisher'da ki exchange ile birebir aynı isimde ve türe sahip bir exchange tanımlanmalıdır.
// 2. Adım : Punlisher tarafından routing key'de bulunan değerdeki kuyruğa gönderilen mesajları kendi oluşturduğumuz kuyruğa yönlendirerek tüketmemiz gerekmektedir. Bunun için öncelikle bir kuyruk oluşturulmalıdır!
// 3. Adım