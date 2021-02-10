using Microsoft.EntityFrameworkCore.Migrations;

namespace Eshop_API.Migrations
{
    public partial class OrderDetailsNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                nullable: false,
                oldClrType: typeof(double));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Items",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
