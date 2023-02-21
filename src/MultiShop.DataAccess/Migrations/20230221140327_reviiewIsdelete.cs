using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.DataAccess.Migrations
{
    public partial class reviiewIsdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Reviews",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Reviews");
        }
    }
}
