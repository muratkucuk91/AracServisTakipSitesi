using Microsoft.EntityFrameworkCore.Migrations;

namespace AracServisTakipSitesi.Data.Migrations
{
    public partial class mailguncelleme : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Uyeler",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Uyeler");
        }
    }
}
