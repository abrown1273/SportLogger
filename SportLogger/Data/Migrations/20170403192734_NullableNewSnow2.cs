using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportLogger.Data.Migrations
{
    public partial class NullableNewSnow2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NewSnow72",
                table: "SkiDay",
                defaultValueSql: "0");

            migrationBuilder.AlterColumn<int>(
                name: "NewSnow24",
                table: "SkiDay",
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NewSnow72",
                table: "SkiDay",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NewSnow24",
                table: "SkiDay",
                nullable: true);
        }
    }
}
