using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace OpenShiftScheduler.Migrations
{
    public partial class shift_cycle_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShiftCycleItem",
                columns: table => new
                {
                    ShiftCycleItemId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ShiftSequence = table.Column<int>(nullable: false),
                    ShiftTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShiftCycleItem", x => x.ShiftCycleItemId);
                    table.ForeignKey(
                        name: "FK_ShiftCycleItem_ShiftTypes_ShiftTypeId",
                        column: x => x.ShiftTypeId,
                        principalTable: "ShiftTypes",
                        principalColumn: "ShiftTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShiftCycleItem_ShiftSequence",
                table: "ShiftCycleItem",
                column: "ShiftSequence",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftCycleItem_ShiftTypeId",
                table: "ShiftCycleItem",
                column: "ShiftTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShiftCycleItem");
        }
    }
}
