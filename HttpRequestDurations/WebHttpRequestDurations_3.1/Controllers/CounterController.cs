using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;
using Prometheus.Client.Abstractions;

namespace CoreWebWithoutExtensions_3._1.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        private readonly ICounter _counter;
        private readonly ICounter _counterTs;
        
        public CounterController(IMetricFactory metricFactory)
        {
            _counter =  metricFactory.CreateCounter("my_counter", "some help about this");
            _counterTs = metricFactory.CreateCounter("my_counter_ts", "some help about this", true);
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            _counterTs.Inc(3);
            
            return Ok();
        }
    }
}
