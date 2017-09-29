using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Fiery.Identity.Clients.ImplicitMvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                //.UseUrls("http://localhost:50301")
                .UseStartup<Startup>()
                .Build();
    }
}
