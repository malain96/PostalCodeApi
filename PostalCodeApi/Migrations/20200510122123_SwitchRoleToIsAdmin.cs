using Microsoft.EntityFrameworkCore.Migrations;

namespace PostalCodeApi.Migrations
{
    public partial class SwitchRoleToIsAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Role",
                "Users");

            migrationBuilder.AddColumn<bool>(
                "IsAdmin",
                "Users",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "IsAdmin",
                "Users");

            migrationBuilder.AddColumn<string>(
                "Role",
                "Users",
                "nvarchar(max)",
                nullable: true);
        }
    }
}