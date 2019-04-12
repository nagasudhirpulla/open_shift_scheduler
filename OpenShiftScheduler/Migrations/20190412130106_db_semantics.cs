using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class db_semantics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShiftTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftTypes_Name",
                table: "ShiftTypes",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSkills_Name",
                table: "ShiftSkills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Name",
                table: "Genders",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShiftTypes_Name",
                table: "ShiftTypes");

            migrationBuilder.DropIndex(
                name: "IX_ShiftSkills_Name",
                table: "ShiftSkills");

            migrationBuilder.DropIndex(
                name: "IX_Genders_Name",
                table: "Genders");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ShiftTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);
        }
    }
}
