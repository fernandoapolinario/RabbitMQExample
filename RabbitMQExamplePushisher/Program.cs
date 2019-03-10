using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQExemplePublisher
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Type something to send to the queue.");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                while (true)
                {
                    var message = Console.ReadLine();

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine("Message sent. \n");
                }
            }
        }
    }
}
