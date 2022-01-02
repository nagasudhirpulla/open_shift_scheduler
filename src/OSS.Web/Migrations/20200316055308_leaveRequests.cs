using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OSS.Web.Migrations;

public partial class leaveRequests : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "LeaveRequests",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CreatedById = table.Column<string>(nullable: true),
                Created = table.Column<DateTime>(nullable: false),
                LastModifiedById = table.Column<string>(nullable: true),
                LastModified = table.Column<DateTime>(nullable: true),
                StartDate = table.Column<DateTime>(nullable: false),
                EndDate = table.Column<DateTime>(nullable: false),
                EmployeeId = table.Column<string>(nullable: true),
                Remarks = table.Column<string>(nullable: true),
                IsExecuted = table.Column<bool>(nullable: false),
                ExecutedAt = table.Column<DateTime>(nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                table.ForeignKey(
                    name: "FK_LeaveRequests_AspNetUsers_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LeaveRequests_AspNetUsers_EmployeeId",
                    column: x => x.EmployeeId,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LeaveRequests_AspNetUsers_LastModifiedById",
                    column: x => x.LastModifiedById,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "LeaveRequestComments",
            columns: table => new
            {
                Id = table.Column<int>(nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                CreatedById = table.Column<string>(nullable: true),
                Created = table.Column<DateTime>(nullable: false),
                LastModifiedById = table.Column<string>(nullable: true),
                LastModified = table.Column<DateTime>(nullable: true),
                Comment = table.Column<string>(nullable: true),
                LeaveRequestId = table.Column<int>(nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_LeaveRequestComments", x => x.Id);
                table.ForeignKey(
                    name: "FK_LeaveRequestComments_AspNetUsers_CreatedById",
                    column: x => x.CreatedById,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LeaveRequestComments_AspNetUsers_LastModifiedById",
                    column: x => x.LastModifiedById,
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_LeaveRequestComments_LeaveRequests_LeaveRequestId",
                    column: x => x.LeaveRequestId,
                    principalTable: "LeaveRequests",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRequestComments_CreatedById",
            table: "LeaveRequestComments",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRequestComments_LastModifiedById",
            table: "LeaveRequestComments",
            column: "LastModifiedById");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRequestComments_LeaveRequestId",
            table: "LeaveRequestComments",
            column: "LeaveRequestId");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRequests_CreatedById",
            table: "LeaveRequests",
            column: "CreatedById");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRequests_LastModifiedById",
            table: "LeaveRequests",
            column: "LastModifiedById");

        migrationBuilder.CreateIndex(
            name: "IX_LeaveRequests_EmployeeId_StartDate_EndDate",
            table: "LeaveRequests",
            columns: new[] { "EmployeeId", "StartDate", "EndDate" },
            unique: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "LeaveRequestComments");

        migrationBuilder.DropTable(
            name: "LeaveRequests");
    }
}
