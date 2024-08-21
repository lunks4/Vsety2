using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vsety.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IC2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_UserId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_UserId",
                table: "Persons");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Users_Id",
                table: "Persons",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_Id",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Users");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UserId",
                table: "Persons",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Users_UserId",
                table: "Persons",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
