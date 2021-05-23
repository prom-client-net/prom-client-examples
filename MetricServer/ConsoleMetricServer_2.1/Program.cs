using System;
using Prometheus.Client;
using Prometheus.Client.Collectors;
using Prometheus.Client.MetricServer;

namespace ConsoleMetricServer
{
    class Program
    {
        static void Main()
        {
            // You can use DefaultFactory
            IMetricServer metricServer = new MetricServer();
            metricServer.Start();

            var counter = Metrics.DefaultFactory.CreateCounter("test_count", "helptext");
            counter.Inc();

            Console.WriteLine("Press any key..");
            Console.ReadKey();
            metricServer.Stop();
        }
    }
}
