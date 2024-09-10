using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vsety.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ic99 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Imgs_logoId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_logoId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "logoId",
                table: "Posts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "logoId",
                table: "Posts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_logoId",
                table: "Posts",
                column: "logoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Imgs_logoId",
                table: "Posts",
                column: "logoId",
                principalTable: "Imgs",
                principalColumn: "Id");
        }
    }
}
