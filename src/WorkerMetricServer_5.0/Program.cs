using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Prometheus.Client.Collectors;
using Prometheus.Client.DependencyInjection;
using Prometheus.Client.MetricServer;

namespace WorkerMetricServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                {
                    services.AddMetricFactory();
                    services.AddSingleton<IMetricServer>(sp => new MetricServer(
                        new MetricServerOptions
                        {
                            CollectorRegistryInstance = sp.GetRequiredService<ICollectorRegistry>(),
                            UseDefaultCollectors = true
                        }));
                    services.AddHostedService<Worker>();
                }).Build();

            var metricServer = host.Services.GetRequiredService<IMetricServer>();

            try
            {
                metricServer.Start();
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Host Terminated Unexpectedly");
            }
            finally
            {
                metricServer.Stop();
            }
        }
    }
}
