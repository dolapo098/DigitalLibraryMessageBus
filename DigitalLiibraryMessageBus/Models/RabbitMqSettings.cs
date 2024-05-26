using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLiibraryMessageBus.Models
{
    public class RabbitMqSettings
    {
        public string HostName { get; set; } = "localhost";
        public string Exchange { get; set; } = "service-comm";
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
    }
}
