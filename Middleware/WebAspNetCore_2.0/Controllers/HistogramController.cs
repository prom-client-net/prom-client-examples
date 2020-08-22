using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;
using Prometheus.Client.Abstractions;

namespace WebAspNetCore.Controllers
{
    [Route("[controller]")]
    public class HistogramController : Controller
    {
        
        // Write HELP and Type only
        /* 
         * # HELP test_hist help_text
         * # TYPE test_hist histogram
         */
        private readonly IMetricFamily<IHistogram> _histogram;

        public HistogramController(IMetricFactory metricFactory)
        {
            _histogram = metricFactory.CreateHistogram("test_hist", "help_text", "params1", "params2");
        }

        [HttpGet("1")]
        public IActionResult Get1()
        {
            return Ok();
        }
    
        
        [HttpGet("2")]
        public IActionResult Get2()
        {
            _histogram.Observe(1); // No Crash
            _histogram.WithLabels("test1", "value1").Observe(1);
            _histogram.WithLabels("test2", "value2").Observe(2);
            return Ok();
        }
    }
}
