using System.Web.Http;
using Owin;
using Prometheus.Client.Owin;

namespace OwinSelfhost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();

            appBuilder.UseWebApi(config);
            appBuilder.UsePrometheusServer();
        }
    }
}
