using Microsoft.AspNetCore.Mvc;

namespace WebMetricPusher.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        private readonly ICounter _counter;

        public CounterController(IMetricFactory metricFactory)
        {
            this._counter = metricFactory.CreateCounter("my_counter", "some help about this");
        }

        [HttpGet]
        public IActionResult Get()
        {
            this._counter.Inc();

            return this.Ok();
        }
    }
}
