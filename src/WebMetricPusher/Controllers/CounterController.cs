using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace WebMetricPusher.Controllers;

[Route("[controller]")]
public class CounterController : Controller
{
    private readonly ICounter _counter;

    public CounterController(IMetricFactory metricFactory)
    {
        _counter = metricFactory.CreateCounter("my_counter", "some help about this");
    }

    [HttpGet]
    public IActionResult Get()
    {
        _counter.Inc();

        return Ok();
    }
}