using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;

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
        private readonly Histogram _histogram = Metrics.CreateHistogram("test_hist", "help_text", "params1");

        
        [HttpGet("1")]
        public IActionResult Get1()
        {
            return Ok();
        }
    
        
        [HttpGet("2")]
        public IActionResult Get2()
        {
            _histogram.Observe(1); // No Crash
            _histogram.Labels("test1").Observe(1);
            _histogram.Labels("test2").Observe(2);
            return Ok();
        }
    }
}
