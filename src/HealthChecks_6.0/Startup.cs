using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Prometheus.Client;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.Collectors;
using Prometheus.Client.HealthChecks;

namespace HealthChecks;

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
        services.AddSingleton<ICollectorRegistry, CollectorRegistry>();
        services.AddSingleton<IMetricFactory, MetricFactory>();
        services
            .AddHealthChecks()
            .AddUrlGroup(new Uri("https://google.com"), "google", HealthStatus.Unhealthy)
            .AddUrlGroup(new Uri("https://invalidurl"), "invalidurl", HealthStatus.Degraded);
        services.AddPrometheusHealthCheckPublisher();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = r => true
        });

        app.UsePrometheusServer();
    }
}
