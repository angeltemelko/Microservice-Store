using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Microservices.Services.ProductAPI.Migrations
{
    /// <inheritdoc />
    public partial class ProductDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Dsecprition = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryName", "Dsecprition", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Category 1", "Description 1", "https://picsum.photos/200/300?random=1", "Product 1", 100.0 },
                    { 2, "Category 2", "Description 2", "https://picsum.photos/200/300?random=2", "Product 2", 200.0 },
                    { 3, "Category 3", "Description 3", "https://picsum.photos/200/300?random=3", "Product 3", 300.0 },
                    { 4, "Category 4", "Description 4", "https://picsum.photos/200/300?random=4", "Product 4", 400.0 },
                    { 5, "Category 5", "Description 5", "https://picsum.photos/200/300?random=5", "Product 5", 500.0 },
                    { 6, "Category 6", "Description 6", "https://picsum.photos/200/300?random=6", "Product 6", 600.0 },
                    { 7, "Category 7", "Description 7", "https://picsum.photos/200/300?random=7", "Product 7", 700.0 },
                    { 8, "Category 8", "Description 8", "https://picsum.photos/200/300?random=8", "Product 8", 800.0 },
                    { 9, "Category 9", "Description 9", "https://picsum.photos/200/300?random=9", "Product 9", 900.0 },
                    { 10, "Category 10", "Description 10", "https://picsum.photos/200/300?random=10", "Product 10", 1000.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
