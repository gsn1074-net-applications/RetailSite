using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailSite.Products.Api.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    DescriptionShort = table.Column<string>(maxLength: 150, nullable: false),
                    DescriptionLong = table.Column<string>(maxLength: 2500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    DescriptionShort = table.Column<string>(maxLength: 150, nullable: false),
                    DescriptionLong = table.Column<string>(maxLength: 2500, nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DescriptionLong", "DescriptionShort", "Name" },
                values: new object[,]
                {
                    { 1, "Fried rice dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Fried rice dishes short description.", "Fried Rice Dishes" },
                    { 2, "Noodle dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Noodle dishes short description.", "Noodle Dishes" },
                    { 3, "Curry dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Curry dishes short description.", "Curry Dishes" },
                    { 4, "Noodle dishes long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Noodle dishes short description.", "\"Lite\" Dishes" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "DescriptionLong", "DescriptionShort", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Stir-fried Jasmine white rice, scrambled egg, onion, scallion and tomato.", "Koa Pad", 6.95m },
                    { 2, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Stir-fried thin rice noodles with chopped shallot, chopped turnip, scrambled egg, scallion, bean sprouts, fried tofu and crushed peanuts.", "Royal Pad Thai", 6.95m },
                    { 3, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Stir-fried thin rice noodles with scrambled egg, broccoli, Napa cabbage and carrot in a sweet black soy sauce.", "Pad See Ew", 6.95m },
                    { 4, 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "Stir-fried thin rice noodles with onion, bell pepper, mushroom, carrot and sweet Thai basil in a spicy and sweet chili sauce.", "Pad Kee Maow (Drunken Noodles)", 6.95m },
                    { 5, 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed fermentum metus sed tincidunt ullamcorper. Nullam dignissim massa non magna tristique, in condimentum quam ultricies. Pellentesque pretium orci sem, vitae ornare elit tincidunt ut. Pellentesque tortor mi, volutpat nec lectus in, egestas mollis risus. Sed aliquam tristique mollis. Fusce vel sapien nec odio iaculis suscipit non nec justo. Nunc egestas elit a nunc venenatis auctor. Suspendisse tellus neque, viverra eu massa sit amet, fringilla finibus velit. Nam at sapien sapien. Vivamus venenatis lorem urna, eget posuere massa lobortis eget.", "A mild mussamun curry paste simmered in creamy coconut milk with bell pepper, potato, carrot, broccoli and peanuts.", "Mussamun Curry", 6.95m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
