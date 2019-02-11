using System;
using System.Threading;
using Prometheus.Client;
using Prometheus.Client.Collectors;
using Prometheus.Client.MetricPusher;

namespace CoreConsoleMetricPusher
{
    internal class Program
    {
        internal static void Main(string[] args)
        {                     
            var defaultPusher = new MetricPusher("http://localhost:9091", "pushgateway-testworker", "default");
            var registry = new CollectorRegistry();
            var customPusher = new MetricPusher(registry, "http://localhost:9091", "pushgateway-testworker", "custom", null, null);
            
            var counter = Metrics.CreateCounter("example_counter1", "help");
            var counterInCustom =  Metrics.WithCustomRegistry(registry).CreateCounter("example_counter2", "help1");
            
            IMetricPushServer server = new MetricPushServer(new IMetricPusher[]
            {
                defaultPusher, customPusher
            });
            
            server.Start();

            for (int i = 0; i < 10; i++)
            {
                counter.Inc();
                counterInCustom.Inc(2);
                
                Console.WriteLine("count: " + i);
                Thread.Sleep(2000);
            }
            
            server.Stop();
        }
    }
}
