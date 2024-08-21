using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vsety.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ic14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Imgs_Persons_PersonId",
                table: "Imgs");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_UserId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Imgs_PersonId",
                table: "Imgs");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Imgs");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Persons",
                newName: "ImgId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_UserId",
                table: "Persons",
                newName: "IX_Persons_ImgId");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Imgs_ImgId",
                table: "Persons",
                column: "ImgId",
                principalTable: "Imgs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Imgs_ImgId",
                table: "Persons");

            migrationBuilder.RenameColumn(
                name: "ImgId",
                table: "Persons",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Persons_ImgId",
                table: "Persons",
                newName: "IX_Persons_UserId");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Imgs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Imgs_PersonId",
                table: "Imgs",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Imgs_Persons_PersonId",
                table: "Imgs",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

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
