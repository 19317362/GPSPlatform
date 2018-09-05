﻿using GPS.IdentityServer4Grain;
using GPS.IdentityServer4Grain.Configs;
using GPS.IdentityServer4Grain.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GPS.IdentityServer4GrainServer
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var serverHostBuilder = new SiloHostBuilder()
                        .UseLocalhostClustering()
                        .ConfigureAppConfiguration((hostingContext, config) =>
                            {
                                config.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
                                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .AddJsonFile($"appsettings.{ hostingContext.HostingEnvironment}.json", optional: true, reloadOnChange: true);
                                config.AddEnvironmentVariables();
                            })
                        .Configure<ClusterOptions>(options =>
                          {
                              options.ClusterId = "dev";
                              options.ServiceId = "IdentityGrainApp";
                          })
                        .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                        .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(IdentityGrainImpl).Assembly).WithReferences())
                        .ConfigureLogging(logging => logging.AddConsole())
                        .ConfigureServices((hostContext, services) => 
                        {
                            services.AddDbContext<GPSIdentityServerDbContext>();
                            services.AddSingleton(hostContext.Configuration);
                            services.Configure<JwtOptions>(hostContext.Configuration.GetSection("JwtOptions"));
                        })
                        .AddMemoryGrainStorageAsDefault()
                        .Build();

            await serverHostBuilder.StartAsync();
            Console.Read();
            await serverHostBuilder.StopAsync();
        }
    }
}
