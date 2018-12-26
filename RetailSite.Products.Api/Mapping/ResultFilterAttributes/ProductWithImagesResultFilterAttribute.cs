using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RetailSite.Products.Api.Mapping.ResultFilterAttributes
{
	public class ProductWithImagesResultFilterAttribute : ResultFilterAttribute
	{
		public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var resultFromAction = context.Result as ObjectResult;

			if(resultFromAction?.Value == null || resultFromAction.StatusCode < 200 || resultFromAction.StatusCode >= 300) //nothing to map
			{
				await next();
				return;
			}

			var (product, images) = ((DAL.Entities.Product, IEnumerable<DTO.Backend.ProductImage>)) resultFromAction.Value;

			var mappedProduct = Mapper.Map<DTO.Read.ProductWithImages>(product);

			resultFromAction.Value = Mapper.Map(images, mappedProduct);

			await next();
		}
	}
}
