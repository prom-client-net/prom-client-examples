using Microsoft.AspNetCore.Mvc;
using Prometheus.Client.Abstractions;

namespace CoreWebWithoutExtensions_3._1.Controllers
{
    [Route("[controller]")]
    public class CounterController : Controller
    {
        private readonly ICounter _counter;
        private readonly IMetricFamily<ICounter> _counterFamily;
        private readonly IMetricFamily<ICounter, (string Controller, string Action)> _counterFamilyTuple;

        public CounterController(IMetricFactory metricFactory)
        {
            _counter = metricFactory.CreateCounter("my_counter", "some help about this");
            _counterFamily = metricFactory.CreateCounter("my_counter_ts", "some help about this", true, "label1", "label2");
            _counterFamilyTuple = metricFactory.CreateCounter("my_counter_tuple", "some help about this", ("Controller", "Action"), true);
        }

        [HttpGet]
        public IActionResult Get()
        {
            _counter.Inc();
            _counterFamily.WithLabels("value1", "value2").Inc(3);
            _counterFamilyTuple.WithLabels(("Counter", "Get")).Inc(5);

            return Ok();
        }
    }
}
