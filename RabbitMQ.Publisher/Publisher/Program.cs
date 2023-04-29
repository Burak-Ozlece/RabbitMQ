using RabbitMQ.Client;
using System.Text;
var exchangeName = "direct-exchange-example";
var routingKeyName = "direct-queue-example";
ConnectionFactory factory = new(); 
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

 using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct);

while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: exchangeName, 
        routingKey: routingKeyName, 
        body: byteMessage);
}


Console.Read();