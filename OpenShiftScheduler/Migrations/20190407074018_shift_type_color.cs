using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class shift_type_color : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorInt",
                table: "ShiftTypes");

            migrationBuilder.AddColumn<string>(
                name: "ColorString",
                table: "ShiftTypes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorString",
                table: "ShiftTypes");

            migrationBuilder.AddColumn<int>(
                name: "ColorInt",
                table: "ShiftTypes",
                nullable: false,
                defaultValue: 0);
        }
    }
}
