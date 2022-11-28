using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Play.Common.Service.MassTransit
{
    public static class MassTransitExtensions
    {
        public static IServiceCollection AddRabbitMQMassTransit(this IServiceCollection services)
        {
            //services.AddMassTransit(x =>
            //{
            //    x.UsingRabbitMq((cxt, cfg) =>
            //    {
            //        cfg.ReceiveEndpoint("catalog-items", e =>
            //        {
            //            e.Consumer<CatalogItemCreatedConsumer>(cxt);
            //            e.Consumer<CatalogItemUpdatedConsumer>(cxt);
            //            e.Consumer<CatalogItemDeletedConsumer>(cxt);
            //        });
            //    });
            //});

            // OPTIONAL, but can be used to configure the bus options
            services.AddOptions<MassTransitHostOptions>()
                .Configure(options =>
                {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(10);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
                });

            return services;
        }
    }
}
