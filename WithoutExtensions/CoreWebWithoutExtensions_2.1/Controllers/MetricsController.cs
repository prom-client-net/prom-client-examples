using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;
using Prometheus.Client.Collectors;

namespace CoreWebWithoutExtensions.Controllers
{
    [Route("[controller]")]
    public class MetricsController : Controller
    {
        [HttpGet]
        public void Get()
        {
            Response.StatusCode = 200;
            using (var outputStream = Response.Body)
            {
                ScrapeHandler.Process(CollectorRegistry.Instance, outputStream);
            }
        }
    }
}