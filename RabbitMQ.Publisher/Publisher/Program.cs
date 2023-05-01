using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new(); //İlk önce ConnectionFactory den bir instance oluşturuy
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic-exchange-example", type: ExchangeType.Topic);

for (int i = 1; i <= 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Mesajın göndereceği topic formatını belirtiniz: ");
    string topic = Console.ReadLine();
    channel.BasicPublish("topic-exchange-example", topic, body: message);
}

Console.Read();