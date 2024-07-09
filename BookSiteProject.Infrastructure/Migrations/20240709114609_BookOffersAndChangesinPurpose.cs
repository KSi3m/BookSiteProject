using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookSiteProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class BookOffersAndChangesinPurpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "typeOfBookOwnership",
                table: "Books");

            migrationBuilder.CreateTable(
                name: "BookOffers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: true),
                    CreatedById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookOffers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookOffers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookOffers_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookOffers_BookId",
                table: "BookOffers",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookOffers_CreatedById",
                table: "BookOffers",
                column: "CreatedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookOffers");

            migrationBuilder.AddColumn<short>(
                name: "Price",
                table: "Books",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "typeOfBookOwnership",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
