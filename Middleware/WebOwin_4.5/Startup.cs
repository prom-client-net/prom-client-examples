using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Prometheus.Client.Owin;
using WebOwin;

[assembly: OwinStartup(typeof(Startup))]

namespace WebOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UsePrometheusServer(q =>
            {
                q.MapPath = "/api/metrics";
            });
            app.UseWebApi(config);
        }
    }
}