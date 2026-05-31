using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HttpClient.Controllers;

[Route("[controller]")]
public class GitHubController(IHttpClientFactory httpClientFactory) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var client = httpClientFactory.CreateClient("GitHub");

        var response = await client.GetAsync("repos/prom-client-net/prom-client");
        var content = await response.Content.ReadAsStringAsync();

        return Content(content, "application/json");
    }
}
