using System.Threading.Tasks;
using System.Collections.Generic;
using RetailSite.Products.Api.DTO.Backend;
using RetailSite.Products.Api.DAL.Entities;

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

		//delete - no async

		void DeleteProduct(Product product);

		//save

		Task<bool> SaveChangesAsync();
	}
}
