using System;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace WebHttpRequestDurations.Controllers;

[Route("[controller]")]
public class CounterController : Controller
{
    private readonly ICounter _counter;
    private readonly ICounter _counterTs;
    private readonly IMetricFamily<ICounter, ValueTuple<string, string>> _conterLabel;
        
    public CounterController(IMetricFactory metricFactory)
    {
        _counter =  metricFactory.CreateCounter("my_counter", "some help about this");
        _conterLabel = metricFactory.CreateCounter("my_counter_label", "some help about this", ("label1","label2"));
        _counterTs = metricFactory.CreateCounter("my_counter_ts", "some help about this", true);
    }
        
    [HttpGet]
    public IActionResult Get()
    { 
        _counter.Inc();
        _counterTs.Inc(3);
        _conterLabel.WithLabels(("my_label", "my_label2")).Inc();
        return Ok();
    }
}