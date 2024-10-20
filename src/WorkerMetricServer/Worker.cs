using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus.Client;

namespace WorkerMetricServer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly ICounter _counter;

    public Worker(ILogger<Worker> logger, IMetricFactory metricFactory)
    {
        _logger = logger;
        _counter = metricFactory.CreateCounter("my_count", "help text");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
            _counter.Inc();
            await Task.Delay(1000, stoppingToken);
        }
    }
}
