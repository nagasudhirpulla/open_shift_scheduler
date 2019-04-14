using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OpenShiftScheduler.Migrations
{
    public partial class dbstart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    GenderId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftGroups",
                columns: table => new
                {
                    ShiftGroupId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftGroups", x => x.ShiftGroupId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftRoles",
                columns: table => new
                {
                    ShiftRoleId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftRoles", x => x.ShiftRoleId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftSkills",
                columns: table => new
                {
                    ShiftSkillId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftSkills", x => x.ShiftSkillId);
                });

            migrationBuilder.CreateTable(
                name: "ShiftTypes",
                columns: table => new
                {
                    ShiftTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    StartOffsetHrs = table.Column<int>(nullable: false),
                    StartOffsetMins = table.Column<int>(nullable: false),
                    RoasterSequence = table.Column<int>(nullable: false),
                    ShiftSequence = table.Column<int>(nullable: false),
                    ColorString = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftTypes", x => x.ShiftTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OfficeId = table.Column<int>(nullable: true),
                    GenderId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 75, nullable: true),
                    Dob = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ShiftRoleId = table.Column<int>(nullable: false),
                    ShiftGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_ShiftGroups_ShiftGroupId",
                        column: x => x.ShiftGroupId,
                        principalTable: "ShiftGroups",
                        principalColumn: "ShiftGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_ShiftRoles_ShiftRoleId",
                        column: x => x.ShiftRoleId,
                        principalTable: "ShiftRoles",
                        principalColumn: "ShiftRoleId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_GenderId",
                table: "Employees",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Name",
                table: "Employees",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftGroupId",
                table: "Employees",
                column: "ShiftGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ShiftRoleId",
                table: "Employees",
                column: "ShiftRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftSkills_ShiftSkillId",
                table: "EmployeeShiftSkills",
                column: "ShiftSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeShiftSkills_EmployeeId_ShiftSkillId",
                table: "EmployeeShiftSkills",
                columns: new[] { "EmployeeId", "ShiftSkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genders_Name",
                table: "Genders",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftGroups_Name",
                table: "ShiftGroups",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftRoles_RoleName",
                table: "ShiftRoles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftSkills_Name",
                table: "ShiftSkills",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftTypes_Name",
                table: "ShiftTypes",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeShiftSkills");

            migrationBuilder.DropTable(
                name: "ShiftTypes");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "ShiftSkills");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "ShiftGroups");

            migrationBuilder.DropTable(
                name: "ShiftRoles");
        }
    }
}
