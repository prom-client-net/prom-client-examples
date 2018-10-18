using System;
using Prometheus.Client;
using Prometheus.Client.MetricServer;

namespace CoreConsoleMetricServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IMetricServer metricServer = new MetricServer("localhost", 9091);
            metricServer.Start();

            var counter = Metrics.CreateCounter("test_count", "helptext");
            counter.Inc();

            Console.WriteLine("Press any key..");
            Console.ReadKey();
            metricServer.Stop();
        }
    }
}
