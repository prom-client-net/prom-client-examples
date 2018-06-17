using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace CoreWebWithoutExtensions.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        private readonly Counter _counter = Metrics.CreateCounter("myCounter", "some help about this");

        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            return Ok();
        }
    }
}
