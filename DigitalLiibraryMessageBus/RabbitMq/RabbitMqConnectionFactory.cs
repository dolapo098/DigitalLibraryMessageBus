using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLiibraryMessageBus.RabbitMq
{
    public class RabbitMqConnectionFactory
    {
        public static IConnection CreateConnection(string hostName)
        {
            var factory = new ConnectionFactory() { HostName = hostName };
            return factory.CreateConnection();
        }

        public static IModel CreateChannel(IConnection connection, string exchange)
        {
            var channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange, ExchangeType.Direct);
            return channel;
        }
    }
}
