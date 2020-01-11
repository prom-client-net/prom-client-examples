using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace CoreWebWithoutExtensions_3._1.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        private readonly Counter _counter = Metrics.CreateCounter("my_counter", "some help about this");
        private readonly Counter _counterTs = Metrics.CreateCounter("my_counter_ts", "some help about this", true);

        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            _counterTs.Inc(3);
            
            return Ok();
        }
    }
}
