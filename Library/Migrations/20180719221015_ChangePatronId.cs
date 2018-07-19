using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class ChangePatronId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Patrons",
                newName: "PatronId");

            migrationBuilder.CreateTable(
                name: "PatronsCopies",
                columns: table => new
                {
                    PatronCopyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CopyId = table.Column<int>(nullable: false),
                    PatronId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronsCopies", x => x.PatronCopyId);
                    table.ForeignKey(
                        name: "FK_PatronsCopies_Copies_CopyId",
                        column: x => x.CopyId,
                        principalTable: "Copies",
                        principalColumn: "CopyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatronsCopies_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "PatronId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatronsCopies_CopyId",
                table: "PatronsCopies",
                column: "CopyId");

            migrationBuilder.CreateIndex(
                name: "IX_PatronsCopies_PatronId",
                table: "PatronsCopies",
                column: "PatronId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PatronId",
                table: "Patrons",
                newName: "Id");

            migrationBuilder.DropTable(
                name: "PatronsCopies");
        }
    }
}
