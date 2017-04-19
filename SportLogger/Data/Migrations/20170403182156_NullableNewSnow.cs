using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportLogger.Data.Migrations
{
    public partial class NullableNewSnow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NewSnow72",
                table: "SkiDay",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AlterColumn<int>(
                name: "NewSnow24",
                table: "SkiDay",
                nullable: true,
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NewSnow72",
                table: "SkiDay",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "NewSnow24",
                table: "SkiDay",
                nullable: false);
        }
    }
}
