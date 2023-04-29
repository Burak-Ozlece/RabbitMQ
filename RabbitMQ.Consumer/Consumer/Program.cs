// Bağlantı oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

var queuName = "example-queue";
ConnectionFactory factory = new();
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

// Bağlantı aktifleştirma
IConnection connection = factory.CreateConnection();

// Kanal oluşturma
IModel channel = connection.CreateModel();

// Queu oluşturma
channel.QueueDeclare(queuName, exclusive: false); //Diğer tarafda nasıl bir yapılandırma varsa aynısı yapılmalı
// Queue'dan mesaj okuma
EventingBasicConsumer consumer = new(channel); //Event tanımlamamız gerek
channel.BasicConsume(queuName, false, consumer); // autoAck: kutruktan alınan mesajı kurukta silip silinmemesi

consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajların işlendiği yer.
    //e.Body : Kuyrukdaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span / e.Body.ToArray() : Kuyrukdaki mesajın byte verisini getirecektir.
    var message = Encoding.UTF8.GetString(e.Body.Span); //byte[] türünden gelen mesajı stringe çeviriyoruz
    Console.WriteLine(message); 
};

Console.Read();