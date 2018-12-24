using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSite.Products.Api.DAL.Entities
{
	[Table("Categories")]
	public class Category
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(150)]
		public string Name { get; set; }

		[Required]
		[MaxLength(150)]
		public string DescriptionShort { get; set; }

		[Required]
		[MaxLength(2500)]
		public string DescriptionLong { get; set; }
	}
}
