using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.Collectors;
using Prometheus.Client.HealthChecks;

namespace HealthChecks_3._1
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
            services.AddHealthChecks();
            services.AddHealthChecks()
                .AddUrlGroup(new Uri("https://google.com"), "google", HealthStatus.Degraded);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UsePrometheusServer()
                .UseHealthChecksPrometheusExporter();

            /*var cr = new CollectorRegistry();

            app.UsePrometheusServer(q =>
                {
                    q.UseDefaultCollectors = false;
                    q.CollectorRegistryInstance = cr;
                    q.MapPath = "/hc-metrics";
                })
                .UseHealthChecksPrometheusExporter(cr);*/
        }
    }
}
