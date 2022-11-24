using MassTransit;
using Play.Catalog.Service.Settings;
using Play.Common.Service.Settings;
using System.Xml.Linq;

namespace Play.Catalog.Service.Extensions
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddRabbitMQMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    var rabbitMQSettings = configuration.GetSection(nameof(RabbitMQSettings)).Get<RabbitMQSettings>();
                    configurator.Host(rabbitMQSettings?.Host);

                    var mongoSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                    configurator.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter(mongoSettings?.CollectionName, false));
                });
            });

            //services.AddMassTransitHostedService();

            return services;
        }
    }
}
