using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prometheus.Client.AspNetCore;
using Prometheus.Client.HttpClient;

namespace HttpClient;

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

        services.AddHttpClient("GitHub", client =>
            {
                client.BaseAddress = new System.Uri("https://api.github.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Prometheus.Client.Examples");
            })
            .AddHttpClientMetrics();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UsePrometheusServer(q => q.UseDefaultCollectors = false);

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
