using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class part_type_new_attrs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ColorString",
                table: "ShiftParticipationTypes",
                nullable: false,
                defaultValue: "#FFFFFF");

            migrationBuilder.AddColumn<bool>(
                name: "IsBold",
                table: "ShiftParticipationTypes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorString",
                table: "ShiftParticipationTypes");

            migrationBuilder.DropColumn(
                name: "IsBold",
                table: "ShiftParticipationTypes");
        }
    }
}
