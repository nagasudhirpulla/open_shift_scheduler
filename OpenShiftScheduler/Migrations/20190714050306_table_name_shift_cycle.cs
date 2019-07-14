using Microsoft.EntityFrameworkCore.Migrations;

namespace OpenShiftScheduler.Migrations
{
    public partial class table_name_shift_cycle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftCycleItem_ShiftTypes_ShiftTypeId",
                table: "ShiftCycleItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftCycleItem",
                table: "ShiftCycleItem");

            migrationBuilder.RenameTable(
                name: "ShiftCycleItem",
                newName: "ShiftCycleItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShiftCycleItem_ShiftTypeId",
                table: "ShiftCycleItems",
                newName: "IX_ShiftCycleItems_ShiftTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShiftCycleItem_ShiftSequence",
                table: "ShiftCycleItems",
                newName: "IX_ShiftCycleItems_ShiftSequence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftCycleItems",
                table: "ShiftCycleItems",
                column: "ShiftCycleItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftCycleItems_ShiftTypes_ShiftTypeId",
                table: "ShiftCycleItems",
                column: "ShiftTypeId",
                principalTable: "ShiftTypes",
                principalColumn: "ShiftTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftCycleItems_ShiftTypes_ShiftTypeId",
                table: "ShiftCycleItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftCycleItems",
                table: "ShiftCycleItems");

            migrationBuilder.RenameTable(
                name: "ShiftCycleItems",
                newName: "ShiftCycleItem");

            migrationBuilder.RenameIndex(
                name: "IX_ShiftCycleItems_ShiftTypeId",
                table: "ShiftCycleItem",
                newName: "IX_ShiftCycleItem_ShiftTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShiftCycleItems_ShiftSequence",
                table: "ShiftCycleItem",
                newName: "IX_ShiftCycleItem_ShiftSequence");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftCycleItem",
                table: "ShiftCycleItem",
                column: "ShiftCycleItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftCycleItem_ShiftTypes_ShiftTypeId",
                table: "ShiftCycleItem",
                column: "ShiftTypeId",
                principalTable: "ShiftTypes",
                principalColumn: "ShiftTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
