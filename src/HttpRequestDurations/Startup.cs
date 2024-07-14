using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.DependencyInjection;
using Prometheus.Client.HttpRequestDurations;

namespace HttpRequestDurations;

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
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UsePrometheusServer(q => q.UseDefaultCollectors = false);
        app.UsePrometheusRequestDurations(q =>
        {
            q.IncludePath = true;
            q.IncludeMethod = true;
            q.IgnoreRoutesConcrete = new[]
            {
                "/favicon.ico",
                "/robots.txt",
                "/"
            };
            q.IgnoreRoutesStartWith = new[]
            {
                "/swagger"
            };
            q.CustomNormalizePath = new Dictionary<Regex, string>
            {
                { new Regex(@"\/[0-9]{1,}(?![a-z])"), "/id" }
            };

            q.CustomLabels = new Dictionary<string, Func<string>>
            {
                {
                    "application_name", () =>  env.ApplicationName
                },
                {
                    "date", () => DateTime.UtcNow.ToString("yyyy-MM-dd")
                }
            };
        });

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
