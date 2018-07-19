using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class ChangePatronsCopiesToPatronsBooks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatronsCopies");

            migrationBuilder.CreateTable(
                name: "PatronsBooks",
                columns: table => new
                {
                    PatronCopyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    BookId = table.Column<int>(nullable: false),
                    PatronId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatronsBooks", x => x.PatronCopyId);
                    table.ForeignKey(
                        name: "FK_PatronsBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatronsBooks_Patrons_PatronId",
                        column: x => x.PatronId,
                        principalTable: "Patrons",
                        principalColumn: "PatronId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatronsBooks_BookId",
                table: "PatronsBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_PatronsBooks_PatronId",
                table: "PatronsBooks",
                column: "PatronId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatronsBooks");

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
    }
}
