using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Library.Migrations
{
    public partial class BookCopyJoinTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooksCopies",
                columns: table => new
                {
                    BookCopyId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    BookId = table.Column<int>(nullable: false),
                    CopyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksCopies", x => x.BookCopyId);
                    table.ForeignKey(
                        name: "FK_BooksCopies_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksCopies_Copies_CopyId",
                        column: x => x.CopyId,
                        principalTable: "Copies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksCopies_BookId",
                table: "BooksCopies",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksCopies_CopyId",
                table: "BooksCopies",
                column: "CopyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksCopies");
        }
    }
}
