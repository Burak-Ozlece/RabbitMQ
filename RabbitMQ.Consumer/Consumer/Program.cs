using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

IConnection connection = factory.CreateConnection();

IModel channel = connection.CreateModel();

channel.ExchangeDeclare("Exchange1", ExchangeType.Direct); // Publisher tarafında tanımladığımız exchange aynısını burdada tanımlıyoruz

channel.QueueDeclare(queue:"BurakKuyruk",exclusive:false); // Kuyruk oluşturuyoruz rastgele bir ad ile

channel.QueueBind(queue: "BurakKuyruk", exchange: "Exchange1", "Burak"); // Oluşturduğumuzu kuyruğa routing key olaran Burak'ı bind ediyoruz

EventingBasicConsumer consumer = new(channel); // Consumer örneği oluşturyoruz oluşturuyoruz

channel.BasicConsume(queue: "BurakKuyruk", autoAck: true, consumer: consumer); // Consumer burda oluşturuyoruz

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span); // Mesajı alıp string'e çeviyoruz
    Console.WriteLine(message); // Konsola yazdırıyoruz
}; 

Console.ReadLine();
