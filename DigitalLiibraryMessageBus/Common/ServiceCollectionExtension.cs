using DigitalLiibraryMessageBus.Interfaces;
using DigitalLiibraryMessageBus.Models;
using DigitalLiibraryMessageBus.RabbitMq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace DigitalLiibraryMessageBus.Common
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = configuration.GetSection("RabbitMq").Get<RabbitMqSettings>();

            // Register settings
            services.AddSingleton(settings);

            // Create and register connection/channel
            var connection = RabbitMqConnectionFactory.CreateConnection(settings.HostName);
            var channel = RabbitMqConnectionFactory.CreateChannel(connection, settings.Exchange);

            services.AddSingleton<IConnection>(connection);
            services.AddSingleton<IModel>(channel);

            // Register service
            services.AddSingleton<IRabbitMqService>(sp => new RabbitMqService(channel, settings));

            return services;
        }
    }
}
