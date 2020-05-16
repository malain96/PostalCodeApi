using Microsoft.EntityFrameworkCore.Migrations;

namespace PostalCodeApi.Migrations
{
    public partial class RemoveTokenAndAddRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsAdmin",
                "Users");

            migrationBuilder.DropColumn(
                "Token",
                "Users");

            migrationBuilder.AddColumn<string>(
                "Role",
                "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Role",
                "Users");

            migrationBuilder.AddColumn<bool>(
                "IsAdmin",
                "Users",
                "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                "Token",
                "Users",
                "nvarchar(max)",
                nullable: true);
        }
    }
}