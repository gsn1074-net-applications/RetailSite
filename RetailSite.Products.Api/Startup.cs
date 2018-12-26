using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Formatters;
using RetailSite.Products.Api.DAL.Contexts;
using RetailSite.Products.Api.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace RetailSite.Products.Api
{
	public class Startup
	{
		public IConfiguration Configuration { get; private set;}

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

			//This supported legacy serialization where property names are serialized cased as they are in the source class 
			//- not translated to lower case which is the case now
			//.AddJsonOptions(o => {
			//	if(o.SerializerSettings.ContractResolver != null)
			//	{
			//		var castedResolver = o.SerializerSettings.ContractResolver as DefaultContractResolver;
			//		castedResolver.NamingStrategy = null;
			//	}
			//});

			string connectionString = Configuration["ConnectionStrings:ProductsApiConnectionString"];
			services.AddDbContext<ProductsContext>(o => o.UseSqlServer(connectionString));

			services.AddScoped<IProductsRepository, ProductsRepository>();

			services.AddAutoMapper();

			services.AddHttpClient();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseMvc();
		}
	}
}
