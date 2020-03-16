using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Web.Migrations
{
    public partial class leaveRequestApprovalFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "LeaveRequests",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "LeaveRequests");
        }
    }
}
