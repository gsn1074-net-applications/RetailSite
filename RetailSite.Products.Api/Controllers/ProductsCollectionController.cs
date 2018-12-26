using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RetailSite.Products.Api.DAL.Repositories;
using RetailSite.Products.Api.Mapping.ModelBinders;
using RetailSite.Products.Api.Mapping.ResultFilterAttributes;

namespace RetailSite.Products.Api.Controllers
{
	[Route("api/productcollections")]
	[ApiController]
	[ProductsResultFilter]
	public class ProductsCollectionController : ControllerBase
	{
		private IProductsRepository _repo;
		private readonly IMapper _mapper;
		private readonly ILogger _logger;

		public ProductsCollectionController(IProductsRepository repo, IMapper mapper, ILogger<ProductsCollectionController> logger)
		{
			_repo = repo ?? throw new ArgumentNullException(nameof(repo));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		[HttpGet]
		[Route("{productIds}", Name = "GetProducts")]
		public async Task<IActionResult> GetProductCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> productIds)
		{
			try
			{
				IEnumerable<DAL.Entities.Product> products = await _repo.GetProductsAsync(productIds);

				if(productIds.Count() != products.Count())
				{
					return NotFound();
				}

				return Ok(products);
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreateProductCollection([FromBody] IEnumerable<DTO.Create.Product> products)
		{
			try
			{
				//validate input and check categories

				IEnumerable<DAL.Entities.Product> productEntities = _mapper.Map<IEnumerable<DAL.Entities.Product>>(products);

				foreach(DAL.Entities.Product p in productEntities)
				{
					_repo.AddProduct(p);
				}

				await _repo.SaveChangesAsync();

				IEnumerable<DAL.Entities.Product> productsToReturn = await _repo.GetProductsAsync(productEntities.Select(p => p.Id).ToList());

				string productIds = string.Join(",", productsToReturn.Select(p => p.Id));

				return CreatedAtRoute("GetProducts", new { productIds }, productsToReturn);
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}
	}
}
