using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RetailSite.Products.Api.DAL.Repositories;
using RetailSite.Products.Api.Mapping.ResultFilterAttributes;

namespace RetailSite.Products.Api.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private IProductsRepository _repo;
		private readonly IMapper _mapper;	

		public ProductsController(IProductsRepository repo, IMapper mapper)
		{
			_repo = repo ?? throw new ArgumentNullException(nameof(repo));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(repo));
		}

		[HttpGet]
		[ProductsResultFilter]
		public async Task<IActionResult> GetProducts()
		{
			IEnumerable<DAL.Entities.Product> productEntities = await _repo.GetProductsAsync();
			return Ok(productEntities);
		}

		[HttpGet]
		[Route("{id}", Name = "GetProduct")]
		[ProductWithImagesResultFilter]
		public async Task<IActionResult> GetProduct(int id)
		{
			DAL.Entities.Product productEntity = await _repo.GetProductAsync(id);

			if(productEntity == null)
			{
				return NotFound();
			}

			var productImages = await _repo.GetProductImagesAsync(id);

			return Ok((productEntity, productImages));
		}

		[HttpPost]
		[ProductResultFilter]
		public async Task<IActionResult> CreateProduct([FromBody] DTO.Create.Product product) 
		{
			//validate input & check for valid category
			DAL.Entities.Product productEntity = _mapper.Map<DAL.Entities.Product>(product);

			_repo.AddProduct(productEntity);

			await _repo.SaveChangesAsync();

			await _repo.GetProductAsync(productEntity.Id); //refetch so that category is populated

			return CreatedAtRoute("GetProduct", new {id = productEntity.Id}, productEntity);
		}
	
	}
}
