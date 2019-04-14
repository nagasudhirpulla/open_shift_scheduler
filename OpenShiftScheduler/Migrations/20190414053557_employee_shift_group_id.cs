using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class employee_shift_group_id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ShiftGroups_ShiftGroupId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftGroupId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ShiftGroups_ShiftGroupId",
                table: "Employees",
                column: "ShiftGroupId",
                principalTable: "ShiftGroups",
                principalColumn: "ShiftGroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_ShiftGroups_ShiftGroupId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ShiftGroupId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_ShiftGroups_ShiftGroupId",
                table: "Employees",
                column: "ShiftGroupId",
                principalTable: "ShiftGroups",
                principalColumn: "ShiftGroupId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
