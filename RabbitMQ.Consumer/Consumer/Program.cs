// Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");


IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

channel.ExchangeDeclare("header-exchange-example", ExchangeType.Headers); // Publis'de tanımladığımız exchange burdada tanımlıyoruz

Console.Write("Lütfen header value'sunu giriniz: "); // Header'ın value değerini kullanıcıdan alıyoruz.
string value = Console.ReadLine();

string queueName = channel.QueueDeclare().QueueName; // Kuyruğın ismini alıyoruz

channel.QueueBind(queueName, "header-exchange-example", string.Empty, new Dictionary<string, object>
{
    ["no"] = value
}); // Bind işelmini yapıyoruz

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(queueName, true, consumer);

consumer.Received += (sender, e) =>
{
    var message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

Console.Read();