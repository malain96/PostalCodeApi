using Microsoft.EntityFrameworkCore.Migrations;

namespace PostalCodeApi.Migrations
{
    public partial class AddUserToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                "Token",
                "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Token",
                "Users");
        }
    }
}