using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tienda_Online.Migrations
{
    /// <inheritdoc />
    public partial class LocationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_IdProduct",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_IdProduct",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IdProduct",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdProduct",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_IdProduct",
                table: "Orders",
                column: "IdProduct");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_IdProduct",
                table: "Orders",
                column: "IdProduct",
                principalTable: "Products",
                principalColumn: "IdProduct",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
