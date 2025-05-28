using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Web.Migrations;

public partial class PartTypeBgClr : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "BgClrString",
            table: "ShiftParticipationTypes",
            nullable: false,
            defaultValue: "#000000");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "BgClrString",
            table: "ShiftParticipationTypes");
    }
}
