using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");
 using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare("Burak-fanout-exchange", ExchangeType.Fanout); // Fanout exchange tanımlıyoruz

for (int i = 1; i <= 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    channel.BasicPublish("Burak-fanout-exchange", string.Empty, body: message); // Fanout exchange routing key'e bakmadan gönderdiği için, routing key'i boş geçiyoruz
}


Console.Read();