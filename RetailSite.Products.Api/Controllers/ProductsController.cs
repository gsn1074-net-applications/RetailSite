using System;
using AutoMapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.JsonPatch;
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
		private readonly ILogger _logger;

		public ProductsController(IProductsRepository repo, IMapper mapper, ILogger<ProductsController> logger)
		{
			_repo = repo ?? throw new ArgumentNullException(nameof(repo));
			_mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
			_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		//read

		[HttpGet]
		[ProductsResultFilter]
		public async Task<IActionResult> GetProducts()
		{
			try
			{
				return Ok(await _repo.GetProductsAsync());
			}
			catch(Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}

		[HttpGet("{id}", Name = "GetProduct")]
		[ProductWithImagesResultFilter]
		public async Task<IActionResult> GetProduct(int id)
		{
			try
			{
				DAL.Entities.Product productEntity = await _repo.GetProductAsync(id);

				if(productEntity == null)
				{
					return NotFound();
				}

				var productImages = await _repo.GetProductImagesAsync(id);

				return Ok((productEntity, productImages));
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}

		//create

		[HttpPost]
		[ProductResultFilter]
		public async Task<IActionResult> CreateProduct([FromBody] DTO.Create.Product product) 
		{
			try
			{
				if(product == null)
				{
					return BadRequest();
				}

				//TODO: consider fluent validations: github: JeremySkinner/FluentValidation
				if(product.DescriptionShort == product.DescriptionLong)
				{
					ModelState.AddModelError("DescriptionShort", "The provided descriptions should be different.");
				}

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				//TODO: check that category is valid	

				DAL.Entities.Product productEntity = _mapper.Map<DAL.Entities.Product>(product);

				_repo.AddProduct(productEntity);

				await _repo.SaveChangesAsync();

				await _repo.GetProductAsync(productEntity.Id); //refetch so that category is populated

				return CreatedAtRoute("GetProduct", new {id = productEntity.Id}, productEntity);
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}

		//update

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateProduct(int id, [FromBody] DTO.Update.Product product)
		{
			try
			{
				if (product == null)
				{
					return BadRequest();
				}

				//TODO: consider fluent validations: github: JeremySkinner/FluentValidation
				if (product.DescriptionShort == product.DescriptionLong)
				{
					ModelState.AddModelError("DescriptionShort", "The provided descriptions should be different.");
				}

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				//TODO: check that category is valid	

				DAL.Entities.Product productEntity = await _repo.GetProductAsync(id);

				if(productEntity == null)
				{
					return NotFound();
				}

				Mapper.Map(product, productEntity);

				if (! await _repo.SaveChangesAsync())
				{
					return StatusCode(500, "A problem happened while handling your request.");
				}

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}

		[HttpPatch("{id}")]
		public async Task<IActionResult> UpdateProductPartial(int id, [FromBody] JsonPatchDocument<DTO.Update.Product> patchDocument)
		{
			try
			{
				if(patchDocument == null)
				{
					return BadRequest();
				}

				DAL.Entities.Product productEntity = await _repo.GetProductAsync(id);

				if (productEntity == null)
				{
					return NotFound();
				}

				DTO.Update.Product productToUpdate = Mapper.Map<DTO.Update.Product>(productEntity);

				patchDocument.ApplyTo(productToUpdate, ModelState);

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				//TODO: consider fluent validations: github: JeremySkinner/FluentValidation
				if (productToUpdate.DescriptionShort == productToUpdate.DescriptionLong)
				{
					ModelState.AddModelError("DescriptionShort", "The provided descriptions should be different.");
				}

				//TODO: check that category is valid

				TryValidateModel(productToUpdate);

				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				Mapper.Map(productToUpdate, productEntity);

				if (!await _repo.SaveChangesAsync())
				{
					return StatusCode(500, "A problem happened while handling your request.");
				}

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}

		//delete

		[HttpDelete]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			try
			{
				DAL.Entities.Product productToDelete = await _repo.GetProductAsync(id);

				if(productToDelete == null)
				{
					return NotFound();
				}

				_repo.DeleteProduct(productToDelete);

				if(! await _repo.SaveChangesAsync())
				{
					return StatusCode(500, "A problem occurred while handling your request.");
				}

				return NoContent();
			}
			catch (Exception ex)
			{
				_logger.LogCritical("error message here", ex);
				return StatusCode(500, "error message here");
			}
		}
	}
}
