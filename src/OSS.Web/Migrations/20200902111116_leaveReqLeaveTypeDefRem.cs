using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Web.Migrations
{
    public partial class leaveReqLeaveTypeDefRem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveRequests",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValue: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "LeaveTypeId",
                table: "LeaveRequests",
                type: "integer",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int));
        }
    }
}
