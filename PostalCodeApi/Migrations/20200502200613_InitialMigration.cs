using Microsoft.EntityFrameworkCore.Migrations;

namespace PostalCodeApi.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Cities",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Cities", x => x.Id); });

            migrationBuilder.CreateTable(
                "PostalCodes",
                table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(nullable: false),
                    CountryIso = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_PostalCodes", x => x.Id); });

            migrationBuilder.CreateTable(
                "PostalCodeCities",
                table => new
                {
                    PostalCodeId = table.Column<long>(nullable: false),
                    CityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostalCodeCities", x => new {x.PostalCodeId, x.CityId});
                    table.ForeignKey(
                        "FK_PostalCodeCities_Cities_CityId",
                        x => x.CityId,
                        "Cities",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PostalCodeCities_PostalCodes_PostalCodeId",
                        x => x.PostalCodeId,
                        "PostalCodes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_PostalCodeCities_CityId",
                "PostalCodeCities",
                "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "PostalCodeCities");

            migrationBuilder.DropTable(
                "Cities");

            migrationBuilder.DropTable(
                "PostalCodes");
        }
    }
}