using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RetailSite.Products.Api.Mapping.ResultFilterAttributes
{
	public class ProductsResultFilterAttribute : ResultFilterAttribute
	{
		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var resultFromAction = context.Result as ObjectResult;

			if(resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300) //nothing to map
			{
				await next();
				return;
			}

			resultFromAction.Value = Mapper.Map<IEnumerable<DTO.Read.Product>>(resultFromAction.Value);

			await next();
		}
	}
}
