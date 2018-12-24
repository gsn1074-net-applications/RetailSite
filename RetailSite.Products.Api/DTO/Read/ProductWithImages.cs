using System.Collections.Generic;

namespace RetailSite.Products.Api.DTO.Read
{
	public class ProductWithImages : Product
	{
		public IEnumerable<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
	}
}
