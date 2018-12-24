using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RetailSite.Products.Api.DAL.Contexts;

namespace RetailSite.Products.Api
{
	public class Program
	{
		public static void Main(string[] args)
		{
			//TODO: remove -- reducing available threads for load testing
			ThreadPool.SetMaxThreads(Environment.ProcessorCount, Environment.ProcessorCount);

			var host = CreateWebHostBuilder(args).Build();

			using(var scope = host.Services.CreateScope())
			{
				try
				{
					var context = scope.ServiceProvider.GetService<ProductsContext>();
					context.Database.Migrate();
				}
				catch (Exception ex)
				{
					var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while migrating the database.");
				}
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
