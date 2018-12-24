using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSite.Products.Api.DTO.Backend
{
	public class ProductImage
	{
		public string Name { get; set; }

		public byte[] Content { get; set; }
	}
}
