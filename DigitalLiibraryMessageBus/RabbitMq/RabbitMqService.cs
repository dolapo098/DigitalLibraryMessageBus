using DigitalLiibraryMessageBus.Interfaces;
using DigitalLiibraryMessageBus.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalLiibraryMessageBus.RabbitMq
{
    public class RabbitMqService : IRabbitMqService
    {
        private readonly IModel _channel;
        private readonly RabbitMqSettings _settings;

        public RabbitMqService(IModel channel, RabbitMqSettings settings)
        {
            _channel = channel;
            _settings = settings;

            _channel.QueueDeclare(_settings.QueueName, durable: true, exclusive: false, autoDelete: false);
            _channel.QueueBind(_settings.QueueName, _settings.Exchange, _settings.RoutingKey);
        }

        public void Publish(string message)
        {
            var body = Encoding.UTF8.GetBytes(message);
            var props = _channel.CreateBasicProperties();
            props.Persistent = true;

            _channel.BasicPublish(exchange: _settings.Exchange,
                                  routingKey: _settings.RoutingKey,
                                  basicProperties: props,
                                  body: body);
        }

        public void Consume(Action<string> handleMessage)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var msg = Encoding.UTF8.GetString(ea.Body.ToArray());
                handleMessage(msg);
            };

            _channel.BasicConsume(queue: _settings.QueueName, autoAck: true, consumer: consumer);
        }
    }
}

