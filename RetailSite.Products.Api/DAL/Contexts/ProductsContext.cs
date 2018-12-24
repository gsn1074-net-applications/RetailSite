using Microsoft.EntityFrameworkCore;
using RetailSite.Products.Api.DAL.Entities;

namespace RetailSite.Products.Api.DAL.Contexts
{
	public class ProductsContext : DbContext
	{
		public DbSet<Product> Products { get; set; }

		public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) {}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			//seed database
			modelBuilder.Entity<Category>().HasData(
				new Category() 
				{
					Id = 1,
					Name = "Fried Rice Dishes",
					DescriptionShort = "Fried rice dishes short description.",
					DescriptionLong = "Fried rice dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget."
				},
				new Category() 
				{
					Id = 2,
					Name = "Noodle Dishes",
					DescriptionShort = "Noodle dishes short description.",
					DescriptionLong = "Noodle dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget."
				},
				new Category() 
				{
					Id = 3,
					Name = "Curry Dishes",
					DescriptionShort = "Curry dishes short description.",
					DescriptionLong = "Curry dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget."
				},
				new Category() 
				{
					Id = 4,
					Name = "\"Lite\" Dishes",
					DescriptionShort = "Noodle dishes short description.",
					DescriptionLong = "Noodle dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget."
				}		
			);

			modelBuilder.Entity<Product>().HasData(
				new Product()
				{
					Id = 1,
					Name = "Koa Pad",
					DescriptionShort = "Stir-fried Jasmine white rice, scrambled egg, onion, scallion and tomato.",
					DescriptionLong = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.",
					Price = 6.95m,
					CategoryId = 1,
				},
				new Product()
				{
					Id = 2,
					Name = "Royal Pad Thai",
					DescriptionShort = "Stir-fried thin rice noodles with chopped shallot, chopped turnip, scrambled egg, scallion, bean sprouts, fried tofu and crushed peanuts.",
					DescriptionLong = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.",
					Price = 6.95m,
					CategoryId = 2,
				},
				new Product()
				{
					Id = 3,
					Name = "Pad See Ew",
					DescriptionShort = "Stir-fried thin rice noodles with scrambled egg, broccoli, Napa cabbage and carrot in a sweet black soy sauce.",
					DescriptionLong = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.",
					Price = 6.95m,
					CategoryId = 2,
				},
				new Product()
				{
					Id = 4,
					Name = "Pad Kee Maow (Drunken Noodles)",
					DescriptionShort = "Stir-fried thin rice noodles with onion, bell pepper, mushroom, carrot and sweet Thai basil in a spicy and sweet chili sauce.",
					DescriptionLong = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.",
					Price = 6.95m,
					CategoryId = 2,
				},
				new Product()
				{
					Id = 5,
					Name = "Mussamun Curry",
					DescriptionShort = "A mild mussamun curry paste simmered in creamy coconut milk with bell pepper, potato, carrot, broccoli and peanuts.",
					DescriptionLong = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.",
					Price = 6.95m,
					CategoryId = 3,
				}
			);

			base.OnModelCreating(modelBuilder);
		}
	}
}
