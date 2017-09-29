using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Fiery.Api.Resources
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.Title = "Resources API";

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://localhost:50201")
                .UseStartup<Startup>()
                .Build();
    }
}
