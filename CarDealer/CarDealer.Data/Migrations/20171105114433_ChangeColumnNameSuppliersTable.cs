
namespace CarDealer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangeColumnNameSuppliersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImorter",
                table: "Suppliers");

            migrationBuilder.AddColumn<bool>(
                name: "IsImporter",
                table: "Suppliers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsImporter",
                table: "Suppliers");

            migrationBuilder.AddColumn<bool>(
                name: "IsImorter",
                table: "Suppliers",
                nullable: false,
                defaultValue: false);
        }
    }
}
