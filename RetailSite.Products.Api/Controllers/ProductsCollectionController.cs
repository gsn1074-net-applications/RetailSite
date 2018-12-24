using System;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

		public ProductsCollectionController(IProductsRepository repo, IMapper mapper)
		{
			_repo = repo ?? throw new ArgumentNullException(nameof(repo));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
		}

		[HttpGet]
		[Route("{productIds}", Name = "GetProducts")]
		public async Task<IActionResult> GetProductCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<int> productIds)
		{
			IEnumerable<DAL.Entities.Product> products = await _repo.GetProductsAsync(productIds);

			if(productIds.Count() != products.Count())
			{
				return NotFound();
			}

			return Ok(products);
		}

		[HttpPost]
		public async Task<IActionResult> CreateProductCollection([FromBody] IEnumerable<DTO.Create.Product> products)
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
	}
}
