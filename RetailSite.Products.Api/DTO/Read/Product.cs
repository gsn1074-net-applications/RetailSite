namespace RetailSite.Products.Api.DTO.Read
{
	public class Product
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string DescriptionShort { get; set; }

		public string DescriptionLong { get; set; }

		public decimal Price { get; set; }

		public string Category { get; set; }

	}
}
