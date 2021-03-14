using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;

namespace WebAspNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var location = Assembly.GetEntryAssembly().Location;
            var directory = Path.GetDirectoryName(location);

            var host = new WebHostBuilder()
                .UseUrls("http://0.0.0.0:5000", "https://0.0.0.0:5001")
                .UseKestrel()
                .UseContentRoot(directory)
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
