using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OpenShiftScheduler.Migrations
{
    public partial class employee_shift_skill : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftSkills_Employees_EmployeeId",
                table: "ShiftSkills");

            migrationBuilder.DropIndex(
                name: "IX_ShiftSkills_EmployeeId",
                table: "ShiftSkills");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ShiftSkills");

            migrationBuilder.CreateTable(
                name: "EmployeeShiftSkills",
                columns: table => new
                {
                    EmployeeShiftSkillId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    ShiftSkillId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeShiftSkills", x => x.EmployeeShiftSkillId);
                    table.UniqueConstraint("IX_EmployeeShiftSkill", x => new { x.EmployeeId, x.ShiftSkillId });
                    table.ForeignKey(
                        name: "FK_EmployeeShiftSkills_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeShiftSkills_ShiftSkills_ShiftSkillId",
                        column: x => x.ShiftSkillId,
                        principalTable: "ShiftSkills",
                        principalColumn: "ShiftSkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftSkills_ShiftSkillId",
                table: "EmployeeShiftSkills",
                column: "ShiftSkillId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeShiftSkills");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ShiftSkills",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSkills_EmployeeId",
                table: "ShiftSkills",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftSkills_Employees_EmployeeId",
                table: "ShiftSkills",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
