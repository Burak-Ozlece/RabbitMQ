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
channel.QueueDeclare(queuName, exclusive: false,durable:true); //Diğer tarafda nasıl bir yapılandırma varsa aynısı yapılmalı
// Queue'dan mesaj okuma
EventingBasicConsumer consumer = new(channel); //Event tanımlamamız gerek
var consumerTag = channel.BasicConsume(queuName, false, consumer); // autoAck: kutruktan alınan mesajı kurukta silip silinmemesi
channel.BasicQos(0, 1, false);

consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajların işlendiği yer.
    //e.Body : Kuyrukdaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray() : Kuyrukdaki mesajın byte verisini getirecektir.
    var message = Encoding.UTF8.GetString(e.Body.Span); //byte[] türünden gelen mesajı stringe çeviriyoruz
    channel.BasicAck(e.DeliveryTag, false); // multiple false değeri vererek sadece bu mesaj için cevap döndürüyoruz.

    //channel.BasicCancel(consumerTag); // Gelen tüm mesajları bu şekilde red edilebiliyor

    //channel.BasicReject(3, true); // Tek bir mesajın işlenmesini reddetmek
    Console.WriteLine(message); 
};

Console.Read();