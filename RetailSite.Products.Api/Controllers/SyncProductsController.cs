using Microsoft.AspNetCore.Mvc;
using RetailSite.Products.Api.DAL.Entities;
using RetailSite.Products.Api.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSite.Products.Api.Controllers
{
	[Route("api/syncProducts")]
	[ApiController]
	public class SyncProductsController : ControllerBase
	{
		private IProductsRepository _repo;

		public SyncProductsController(IProductsRepository repo) {
			_repo = repo ?? throw new ArgumentNullException(nameof(repo));
		}

		[HttpGet]
		public IActionResult GetProducts() 
		{
			IEnumerable<Product> productEntities = _repo.GetProducts();
			return Ok(productEntities);
		}

	}
}
