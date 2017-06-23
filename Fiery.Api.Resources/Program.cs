using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace Fiery.Api.Resources
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Console.Title = "Resources API";

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:50201")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
