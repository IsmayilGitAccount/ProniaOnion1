using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProniaOnion.Persistence.Contexts.Migrations
{
    /// <inheritdoc />
    public partial class CreateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductTags_Colors_ColorId",
                table: "ProductTags");

            migrationBuilder.DropIndex(
                name: "IX_ProductTags_ColorId",
                table: "ProductTags");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "ProductTags");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "ProductTags",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductTags_ColorId",
                table: "ProductTags",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductTags_Colors_ColorId",
                table: "ProductTags",
                column: "ColorId",
                principalTable: "Colors",
                principalColumn: "Id");
        }
    }
}
