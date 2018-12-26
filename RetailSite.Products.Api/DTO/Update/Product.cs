using System.ComponentModel.DataAnnotations;

namespace RetailSite.Products.Api.DTO.Update
{
	public class Product
	{
		[Required(ErrorMessage = "Name is required.")]
		[MaxLength(150, ErrorMessage = "Name max length is 150.")]
		public string Name { get; set; }

		[Required(ErrorMessage = "DescriptionShort is required.")]
		[MaxLength(150, ErrorMessage = "DescriptionShort max length is 150")]
		public string DescriptionShort { get; set; }

		[Required(ErrorMessage = "DescriptionLong is required.")]
		[MaxLength(2500, ErrorMessage = "DescriptionLong max length is 2500")]
		public string DescriptionLong { get; set; }

		[Required(ErrorMessage = "Price is required.")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "CategoryId is required.")]
		public int CategoryId { get; set; }
	}
}
