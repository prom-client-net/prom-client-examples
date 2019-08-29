using System;
using Prometheus.Client;
using Prometheus.Client.MetricServer;

namespace CoreConsoleMetricServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MetricServerOptions
            {
                Port = 9091                
            };
            
            IMetricServer metricServer = new MetricServer(Metrics.DefaultCollectorRegistry, _options);
            metricServer.Start();

            var counter = Metrics.CreateCounter("test_count", "helptext");
            counter.Inc();

            Console.WriteLine("Press any key..");
            Console.ReadKey();
            metricServer.Stop();
        }
    }
}
