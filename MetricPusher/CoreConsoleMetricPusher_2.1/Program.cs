using System;
using Prometheus.Client.MetricPusher;

namespace CoreConsoleMetricPusher_2._1
{
    class Program
    {
        static void Main(string[] args)
        {
            MetricPusherWorker.Start();
            Console.WriteLine("Hello World!");

            while (true)
            {
                
            }
        }
    }
}