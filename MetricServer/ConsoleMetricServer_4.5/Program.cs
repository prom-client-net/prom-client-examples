using System;
using Prometheus.Client;
using Prometheus.Client.MetricServer;

namespace ConsoleMetricServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            IMetricServer metricServer = new MetricServer("localhost", 9091, "/met", false);
            metricServer.Start();

            var counter = Metrics.CreateCounter("test_count", "helptext");
            counter.Inc();

            Console.WriteLine("Press any key..");
            Console.ReadKey();
            metricServer.Stop();
        }
    }
}
