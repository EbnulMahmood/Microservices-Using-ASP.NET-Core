using Newtonsoft.Json;
using Play.Catalog.Service.RabbitMQ.Contracts;
using RabbitMQ.Client;
using System.Text;

namespace Play.Catalog.Service.RabbitMQ
{
    public class RabitMQProducer : IRabitMQProducer
    {
        public void SendItemMessage<T>(T message)
        {
            const string itemName = "item";
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare(itemName, exclusive: false);
            //Serialize the message
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);
            //put the data on to the item queue
            channel.BasicPublish(exchange: "", routingKey: itemName, body: body);
        }
    }
}
