﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace OSS.Web.Migrations;

public partial class displayname : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "DisplayName",
            table: "AspNetUsers",
            nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DisplayName",
            table: "AspNetUsers");
    }
}
