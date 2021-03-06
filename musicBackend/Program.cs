using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace musicBackend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("https://0.0.0.0:5000", "https://localhost:5001");
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseKestrel(options => {
                        options.Limits.MaxRequestBodySize = null;
                        options.Limits.MaxRequestBufferSize = null;
                        options.Limits.MaxResponseBufferSize = null;
                        });
                });
    }
}
