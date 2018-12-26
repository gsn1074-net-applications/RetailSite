using System;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using RetailSite.Products.Api.DAL.Contexts;
using Microsoft.Extensions.DependencyInjection;

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
