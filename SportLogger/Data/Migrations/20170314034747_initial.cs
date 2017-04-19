using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SportLogger.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResortReference",
                columns: table => new
                {
                    ResortName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResortReference", x => x.ResortName);
                });

            migrationBuilder.CreateTable(
                name: "SkiDay",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NewSnow24 = table.Column<int>(nullable: false),
                    NewSnow72 = table.Column<int>(nullable: false),
                    Partners = table.Column<string>(nullable: true),
                    Resort = table.Column<string>(nullable: false),
                    SkiDate = table.Column<DateTime>(nullable: false),
                    Temperature = table.Column<decimal>(nullable: false),
                    Vertical = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkiDay", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResortReference");

            migrationBuilder.DropTable(
                name: "SkiDay");
        }
    }
}
