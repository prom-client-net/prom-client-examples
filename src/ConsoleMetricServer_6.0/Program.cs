using System;
using Prometheus.Client;
using Prometheus.Client.Collectors;
using Prometheus.Client.MetricServer;

namespace ConsoleMetricServer;

class Program
{
    static void Main()
    {
        var registry = new CollectorRegistry();
        var options = new MetricServerOptions
        {
            CollectorRegistryInstance = registry
        };
        var factory = new MetricFactory(registry);

        IMetricServer metricServer = new MetricServer(options);
        metricServer.Start();

        var counter = factory.CreateCounter("test_count", "helptext");
        counter.Inc();

        Console.WriteLine("Press any key..");
        Console.ReadKey();
        metricServer.Stop();
    }
}
