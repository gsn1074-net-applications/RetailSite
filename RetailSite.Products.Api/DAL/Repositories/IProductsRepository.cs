using RetailSite.Products.Api.DAL.Entities;
using RetailSite.Products.Api.DTO.Backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSite.Products.Api.DAL.Repositories
{
	public interface IProductsRepository
	{
		//read

		Task<Product> GetProductAsync(int id);

		Task<IEnumerable<Product>> GetProductsAsync();

		Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<int> ids);

		Task<ProductImage> GetProductImageAsync(string name);

		Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);

		//create - no async

		void AddProduct(Product product);

		//update


		//delete


		//save
		Task<bool> SaveChangesAsync();


		//Sync for load testing comparison

		IEnumerable<Product> GetProducts();

		Product GetProduct(int id);

	}
}
