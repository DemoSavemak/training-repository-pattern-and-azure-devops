using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;


namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
           CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
              .ConfigureWebHostDefaults(webBuilder =>
              {
                  webBuilder.ConfigureKestrel(serverOptions =>
                  {
                      serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(120);
                      serverOptions.Limits.RequestHeadersTimeout = TimeSpan.FromMinutes(120);
                  })
                  .UseStartup<Startup>()
                  .ConfigureLogging(builder => builder.AddAzureWebAppDiagnostics());
              });
    }
}
