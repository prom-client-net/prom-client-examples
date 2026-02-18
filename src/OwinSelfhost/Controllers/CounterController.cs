using System.Web.Http;
using Prometheus.Client;

namespace OwinSelfhost.Controllers
{
    [Route("counter")]
    public class CounterController : ApiController
    {
        private readonly ICounter _counter = Metrics.DefaultFactory.CreateCounter("my_counter", "some help about this");
        private readonly IMetricFamily<ICounter> _counterFamily = Metrics.DefaultFactory.CreateCounter("my_counter_ts", "some help about this", true, "label1", "label2");

        [HttpGet]
        public IHttpActionResult Get()
        {
            _counter.Inc();
            _counterFamily.WithLabels("value1", "value2").Inc(3);

            return Ok();
        }
    }
}
