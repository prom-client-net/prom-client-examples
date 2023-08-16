using System;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

namespace WebHttpRequestDurations.Controllers
{



    [Route("[controller]")]
    public class HistogramController : Controller
    {
        private readonly IMetricFamily<IHistogram, ValueTuple<string>> _histogram;

        public HistogramController(IMetricFactory metricFactory)
        {
            _histogram = metricFactory.CreateHistogram("test_hist", "help_text", ValueTuple.Create("params1"));
        }

        [HttpGet]
        public IActionResult Get()
        {
            _histogram.WithLabels("test1").Observe(1);
            _histogram.WithLabels("test2").Observe(2);
            return Ok();
        }
    }
}
