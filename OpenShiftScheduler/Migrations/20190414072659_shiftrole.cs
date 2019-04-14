using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class shiftrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_ShiftRoles_RoleName",
                table: "ShiftRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_ShiftRoles_RoleName",
                table: "ShiftRoles",
                column: "RoleName");
        }
    }
}
