using System.Web.Http;
using Prometheus.Client;

namespace OwinSelfhost.Controllers;

[Route("histogram")]
public class HistogramController : ApiController
{
    private readonly IMetricFamily<IHistogram> _histogram = Metrics.DefaultFactory.CreateHistogram("test_hist", "help_text", "params1", "params2");

    [HttpGet]
    public IHttpActionResult Get()
    {
        _histogram.WithLabels("test1", "value1").Observe(1);
        _histogram.WithLabels("test2", "value2").Observe(2);
        return Ok();
    }
}
