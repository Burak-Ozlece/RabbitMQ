using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new(); //İlk önce ConnectionFactory den bir instance oluşturuy
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");


 using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "header-exchange-example", type: ExchangeType.Headers); // Exchange türünü belirtiyoruz

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Merhaba {i}");
    Console.Write("Lütfen header value'sunu giriniz: "); // header'ın value'sunu kullanıcıdan alıyoruz
    string value = Console.ReadLine();

    IBasicProperties basicProperties = channel.CreateBasicProperties(); // IBasicProperties tanımlıyoruz
    basicProperties.Headers = new Dictionary<string, object> // basicProperties'in headerına dictionary türünde key ve value atıyoruz
    {
        ["no"] = value
    };

    channel.BasicPublish(exchange: "header-exchange-example", routingKey: string.Empty, body: message, basicProperties: basicProperties); //  Basic propertiesi veriyoruz
}



Console.Read();