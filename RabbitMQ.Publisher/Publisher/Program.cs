using RabbitMQ.Client;
using System.Text;

//Bağlantı oluşturma
ConnectionFactory factory = new(); //İlk önce ConnectionFactory den bir instance oluşturuy
factory.Uri = new("amqps://jzmftdtu:3y8Mi9FFRYDQlIuUTOHVEF-xDxUKsvoX@hawk.rmq.cloudamqp.com/jzmftdtu");

//Bağlantıyı aktifleştirme
 using IConnection connection = factory.CreateConnection();

// Kanal oluşturuyoruz
using IModel channel = connection.CreateModel();

//Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false);
// queue: kuyruğun adı, durable: kuyrukdaki mesajların kalıcılığı ile alakalı, exclusive: bu kuyruğun özel olup olmaması, yani bu bağlantının dışında başka bir bağlantı bağlanmıyor true olduğunda,, ayriyetden bağlantı kapandığı zaman o kuyruk imha edilir, autoDelete: kuyruğun içindeki tüm mesajlar tüketildiğinde, kuyruğun silinip silinemiyeceğini talimat sağlar.

// Queue'ya mesaj gönderme

//RabbitMQ kuruğa atacağı mesajları byte türünden kabul etmektedir. Haliyle mesajları byte dönüştürmemiz gerekir.
for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    await Task.Delay(200);
    channel.BasicPublish(exchange: string.Empty, routingKey: "example-queue", body: message); // Exchange belirtmediğimiz zaman direct exhange davranış benimser ve bu exchange varsayılandır.
}


Console.Read();