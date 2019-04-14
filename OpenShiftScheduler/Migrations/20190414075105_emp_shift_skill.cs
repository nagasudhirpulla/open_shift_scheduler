using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class emp_shift_skill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "IX_EmployeeShiftSkill",
                table: "EmployeeShiftSkills");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_EmployeeShiftSkills_EmployeeId_ShiftSkillId",
                table: "EmployeeShiftSkills",
                columns: new[] { "EmployeeId", "ShiftSkillId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_EmployeeShiftSkills_EmployeeId_ShiftSkillId",
                table: "EmployeeShiftSkills");

            migrationBuilder.AddUniqueConstraint(
                name: "IX_EmployeeShiftSkill",
                table: "EmployeeShiftSkills",
                columns: new[] { "EmployeeId", "ShiftSkillId" });
        }
    }
}
