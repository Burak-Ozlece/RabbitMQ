using RabbitMQ.Client;
using System.Text;

ConnectionFactory factory = new(); 
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

 using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "Exchange1", type: ExchangeType.Direct); // Exchange tanımlıyoruz

while (true)
{
    Console.Write("Mesaj: ");
    string message = Console.ReadLine(); 
    byte[] byteMessage = Encoding.UTF8.GetBytes(message); // MEsajı byte'a çaviriyoruz

    channel.BasicPublish(exchange: "Exchange1", routingKey: "Burak", body: byteMessage); // Gönderiyoruz
}
