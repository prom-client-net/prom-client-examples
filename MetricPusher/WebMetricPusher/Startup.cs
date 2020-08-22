using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.Client.Collectors.Abstractions;
using Prometheus.Client.DependencyInjection;
using Prometheus.Client.HttpRequestDurations;
using Prometheus.Client.MetricPusher;

namespace WebMetricPusher
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddMetricFactory();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UsePrometheusRequestDurations(q =>
            {
                q.IncludePath = true;
                q.IncludeMethod = true;
            });

            var registry = app.ApplicationServices.GetService<ICollectorRegistry>();
            var worker = new MetricPushServer( new MetricPusher(registry, "http://localhost:9091", "pushgateway",null, null, null));
            worker.Start();
        }
    }
}
