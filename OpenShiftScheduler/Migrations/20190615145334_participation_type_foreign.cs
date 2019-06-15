using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class participation_type_foreign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShiftParticipationTypeId",
                table: "ShiftParticipations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftParticipations_ShiftParticipationTypeId",
                table: "ShiftParticipations",
                column: "ShiftParticipationTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftParticipations_ShiftParticipationTypes_ShiftParticipat~",
                table: "ShiftParticipations",
                column: "ShiftParticipationTypeId",
                principalTable: "ShiftParticipationTypes",
                principalColumn: "ShiftParticipationTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftParticipations_ShiftParticipationTypes_ShiftParticipat~",
                table: "ShiftParticipations");

            migrationBuilder.DropIndex(
                name: "IX_ShiftParticipations_ShiftParticipationTypeId",
                table: "ShiftParticipations");

            migrationBuilder.DropColumn(
                name: "ShiftParticipationTypeId",
                table: "ShiftParticipations");
        }
    }
}
