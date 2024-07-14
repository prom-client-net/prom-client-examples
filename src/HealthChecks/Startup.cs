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
using Prometheus.Client.DependencyInjection;
using Prometheus.Client.HealthChecks;

namespace HealthChecks;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddMetricFactory();

        services
            .AddHealthChecks()
            .AddUrlGroup(new Uri("https://google.com"), "google", HealthStatus.Unhealthy)
            .AddUrlGroup(new Uri("https://invalidurl"), "invalidurl", HealthStatus.Degraded);
        services.AddPrometheusHealthCheckPublisher();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseHealthChecks("/hc", new HealthCheckOptions
        {
            Predicate = _ => true
        });

        app.UsePrometheusServer();
    }
}
