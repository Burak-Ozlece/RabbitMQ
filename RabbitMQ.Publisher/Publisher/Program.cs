using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new(); //İlk önce ConnectionFactory den bir instance oluşturuy
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

 using IConnection connection = factory.CreateConnection();

using IModel channel = connection.CreateModel();

string queueName = "example-p2p-queue";

channel.QueueDeclare(queueName, exclusive: false, autoDelete: false);

byte[] message = Encoding.UTF8.GetBytes("Merhaba");

channel.BasicPublish("", queueName, body: message);


Console.Read();