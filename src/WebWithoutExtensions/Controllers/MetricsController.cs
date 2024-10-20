using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prometheus.Client;
using Prometheus.Client.Collectors;

namespace WebWithoutExtensions.Controllers;

[Route("[controller]")]
public class MetricsController : Controller
{
    private readonly ICollectorRegistry _registry;

    public MetricsController(ICollectorRegistry registry)
    {
        _registry = registry;
    }

    [HttpGet]
    public async Task Get()
    {
        Response.StatusCode = 200;
        await using var outputStream = Response.Body;
        await ScrapeHandler.ProcessAsync(_registry, outputStream);
    }
}