using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagemnt.Migrations
{
    public partial class Removeunwantedcolumnfrompersontable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlHandle",
                table: "Person");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlHandle",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
