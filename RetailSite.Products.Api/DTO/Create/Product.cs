using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSite.Products.Api.DTO.Create
{
	public class Product
	{
		public string Name { get; set; }

		public string DescriptionShort { get; set; }

		public string DescriptionLong { get; set; }

		public decimal Price { get; set; }

		public int CategoryId { get; set; }
	}
}
