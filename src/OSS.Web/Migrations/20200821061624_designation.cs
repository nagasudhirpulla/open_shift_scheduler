using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Web.Migrations;

public partial class designation : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Designation",
            table: "AspNetUsers",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Designation",
            table: "AspNetUsers");
    }
}
