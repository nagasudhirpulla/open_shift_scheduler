using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Web.Migrations
{
    public partial class partTypeMandatory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ShiftParticipationTypeId",
                table: "ShiftParticipations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ShiftParticipationTypeId",
                table: "ShiftParticipations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
