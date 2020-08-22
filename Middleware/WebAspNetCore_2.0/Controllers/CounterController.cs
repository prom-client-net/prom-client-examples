using Microsoft.AspNetCore.Mvc;
using Prometheus.Client.Abstractions;

namespace WebAspNetCore.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        readonly ICounter _counter;

        public CounterController(IMetricFactory metricFactory)
        {
            _counter = metricFactory.CreateCounter("myCounter", "some help about this");
        }

        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            return Ok();
        }
    }
}
