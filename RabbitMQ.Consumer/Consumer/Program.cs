// Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");
IConnection connection = factory.CreateConnection();
IModel channel = connection.CreateModel();

channel.ExchangeDeclare("Burak-fanout-exchange", type: ExchangeType.Fanout); // Publisher tarafında tanımladığımız exchange'cin aynısı burda tanımlıyoruz

Console.Write("Kuyruk adı giriniz: "); // Kullanıcıdan roting key adı girmesini istiyoruz

string queueName = Console.ReadLine();

channel.QueueDeclare(queueName,exclusive:false); // Kuruğu tanımlıyoruz

channel.QueueBind(queueName, "Burak-fanout-exchange", string.Empty); // Kuyruğu exchange'ce bind ediyoruz

EventingBasicConsumer consumer = new(channel); // Consumer örneği oluşturuyoruz
channel.BasicConsume(queueName, false, consumer); // BasicConsume tanımlıyoruz

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
    channel.BasicAck(e.DeliveryTag, false); // Başarılı bir şekilde okunan mesajı, silmesi için DeliveryTag'ini geri gönderiyoruz
};

Console.Read();