using Newtonsoft.Json;
using Okr.Service.Bus.Model;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Okr.Service.Bus.Repository
{
    public class OkrServiceBus:IOkrServiceBus
    {

            /*  queue: Oluşturulacak olan Queue’nun ismi.
                durable: Bu parametre ile in-memory olarak çalışan Queue disk üzerinden çalışmaya başlayacaktır. 
                    Bu sayede RabbitMQ servisi dursa bile Queue kaybolmayacaktır. Her güzelliğin getirdiği bir 
                    kötü tarafın olduğu gibi bununda beraberinde getireceği latency problemi bulunmaktadır haliyle.
                exchange: Bu parametreyi es geçiyoruz.Exchange genel olarak mesajı ilgili Routing Key’e göre ilgili 
                    Queue’ya yönlendiren bölümdür.Direct Exchange, Fanout Exchange ve Topic Exchange gibi tipleri 
                    bulunmaktadır. Bunları bir sonraki makalemde detaylı olarak ele alacağım.
                routingKey: Burada girmiş olduğumuz key’e göre ilgili Queue’ya yönlendirilecektir mesaj.
                body: Queue’ya göndermek istediğimiz mesajı byte[] tipinde gönderiyoruz.*/

        public string SendMessage(UserBusModel userBusModel)
        {
            try
            {

                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (IConnection connection = factory.CreateConnection())
                {

                    using (IModel channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(
                            queue: "OkrBusQueue",
                            durable: false,
                            exclusive: false,
                            autoDelete: false,
                            arguments: null
                            );

                        string message = JsonConvert.SerializeObject(userBusModel);
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(
                            exchange: "",
                            routingKey: "OkrBusQueue",
                            basicProperties: null,
                            body: body
                            );
                    }

                }

                return "İşlem Başarılı";
            }
            catch (Exception ex)
            {
                return "Hata: " + ex.Message;
            }

        }

        /*noAck: True olarak set edildiği taktirde, consumer mesajı aldığı zaman otomatik olarak mesaj Queue’dan silinecektir.
            Eğer Queue üzerinden silinmesini istemiyor iseniz, False olarak set etmeniz gerekmektedir.*/

        public List<UserBusModel> Consumer(string queueName)
        {

            List<UserBusModel> userBusModelList= new List<UserBusModel>();

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            {

                using (IModel channel = connection.CreateModel())
                {
                    var consumer = new EventingBasicConsumer(channel);
                    channel.BasicConsume(queueName, false, consumer);

                    consumer.Received += (object sender, BasicDeliverEventArgs e) =>
                    {
                        var message = Encoding.UTF8.GetString(e.Body.ToArray());
                        userBusModelList.Add(JsonConvert.DeserializeObject<UserBusModel>(message));

                      
                    };
                }

            }

            return userBusModelList;
        }




    }
}
