using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLiibraryMessageBus.Interfaces
{
    public interface IRabbitMqService
    {
        void Publish(string message);
        void Consume(Action<string> handleMessage);
    }
}
