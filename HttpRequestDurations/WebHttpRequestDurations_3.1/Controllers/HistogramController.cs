using System;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;
using Prometheus.Client.Abstractions;

namespace CoreWebWithoutExtensions_3._1.Controllers
{
    [Route("[controller]")]
    public class HistogramController : Controller
    {
        private readonly IMetricFamily<IHistogram, ValueTuple<string>> _histogram;

        public HistogramController(IMetricFactory metricFactory)
        {
            _histogram = metricFactory.CreateHistogram("test_hist", "help_text", ("params1"));
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            _histogram.WithLabels("test1").Observe((1));
            _histogram.WithLabels("test2").Observe((2));
            return Ok();
        }
    }
}
