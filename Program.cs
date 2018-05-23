using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AppSettingBug
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Works(args).Run();
        }

        // Doesn't Work
        public static IWebHost DoesntWork(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .Build();

            return new WebHostBuilder()
                .UseKestrel()
                .UseConfiguration(config)
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
        
        // Works
        public static IWebHost Works(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .ConfigureAppConfiguration((builder, config) =>
                {
                    config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);
                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
        }
    }
}
