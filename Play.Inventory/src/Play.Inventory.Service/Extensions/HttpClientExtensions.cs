using Play.Inventory.Service.Clients.Contracts;
using Play.Inventory.Service.Clients;
using Polly;
using Polly.Timeout;

namespace Play.Inventory.Service.Extensions
{
    public static class HttpClientExtensions
    {
        private const int _timeoutSeconds = 1;
        private const int _retryCount = 5;


        private const int _allowedRequestBeforeBreaking = 3;
        private const int _awaitCircuitOpenSeconds = 15;

        private static TimeSpan RnadomJitter(int retryAttempt)
        {
            try
            {
                const int retryAttemptRaisedSeconds = 2;
                const int rnadomJitterStart = 0;
                const int rnadomJitterEnd = 1000;

                Random jitterer = new();

                var result = TimeSpan.FromSeconds(Math.Pow(retryAttemptRaisedSeconds, retryAttempt)) +
                                            TimeSpan.FromMilliseconds(jitterer.Next(rnadomJitterStart, rnadomJitterEnd));

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static IServiceCollection AddCatalogHttpClient(this IServiceCollection services, string url)
        {
            services.AddHttpClient<ICatalogClient, CatalogClient>(client =>
                {
                    client.BaseAddress = new Uri(url);
                })
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder
                    .Or<TimeoutRejectedException>()
                    .WaitAndRetryAsync(
                        _retryCount,
                        retryAttempt => RnadomJitter(retryAttempt),
                        onRetry: (outcome, timespan, retryAttempt) =>
                        {
                            Console.WriteLine($"Delaying for {timespan.TotalSeconds} seconds, then making retry {retryAttempt}");
                        }
                ))
                .AddTransientHttpErrorPolicy(policyBuilder => policyBuilder
                    .Or<TimeoutRejectedException>()
                    .CircuitBreakerAsync(
                        _allowedRequestBeforeBreaking,
                        TimeSpan.FromSeconds(_awaitCircuitOpenSeconds),
                        onBreak: (outcome, timespan) =>
                        {
                            Console.WriteLine($"Opening the circuit for {timespan.TotalSeconds} seconds...");
                        },
                        onReset: () =>
                        {
                            Console.WriteLine($"Closing the circuit...");
                        }
                    ))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(_timeoutSeconds));

            return services;
        }
    }
}
