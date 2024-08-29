using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vsety.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ic66 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Imgs_ImgEntity",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserLikes_UserLikesEntityId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_UserReposts_UserRepostsEntityId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Persons_PersonId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Posts_PostEntityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Posts_PostEntityId1",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserLikes");

            migrationBuilder.DropTable(
                name: "UserReposts");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PostEntityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PostEntityId1",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ImgId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserLikesEntityId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_UserRepostsEntityId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Persons_ImgEntity",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostEntityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PostEntityId1",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserLikesEntityId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "UserRepostsEntityId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "ImgEntity",
                table: "Persons");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Posts",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Persons",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Imgs",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "PostEntityUserEntity",
                columns: table => new
                {
                    PostLikesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserLikesId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntityUserEntity", x => new { x.PostLikesId, x.UserLikesId });
                    table.ForeignKey(
                        name: "FK_PostEntityUserEntity_Posts_PostLikesId",
                        column: x => x.PostLikesId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEntityUserEntity_Users_UserLikesId",
                        column: x => x.UserLikesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PostEntityUserEntity1",
                columns: table => new
                {
                    PostRepostsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserRepostsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostEntityUserEntity1", x => new { x.PostRepostsId, x.UserRepostsId });
                    table.ForeignKey(
                        name: "FK_PostEntityUserEntity1_Posts_PostRepostsId",
                        column: x => x.PostRepostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostEntityUserEntity1_Users_UserRepostsId",
                        column: x => x.UserRepostsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImgId",
                table: "Posts",
                column: "ImgId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UserId",
                table: "Persons",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Imgs_PersonId",
                table: "Imgs",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostEntityUserEntity_UserLikesId",
                table: "PostEntityUserEntity",
                column: "UserLikesId");

            migrationBuilder.CreateIndex(
                name: "IX_PostEntityUserEntity1_UserRepostsId",
                table: "PostEntityUserEntity1",
                column: "UserRepostsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Imgs_Persons_PersonId",
                table: "Imgs");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Users_UserId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts");

            migrationBuilder.DropTable(
                name: "PostEntityUserEntity");

            migrationBuilder.DropTable(
                name: "PostEntityUserEntity1");

            migrationBuilder.DropIndex(
                name: "IX_Posts_ImgId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Persons_UserId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Imgs_PersonId",
                table: "Imgs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Imgs");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "PostEntityId",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "PostEntityId1",
                table: "Users",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Posts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserLikesEntityId",
                table: "Posts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "UserRepostsEntityId",
                table: "Posts",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<Guid>(
                name: "ImgEntity",
                table: "Persons",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Comments",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "UserLikes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserLikes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserReposts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReposts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserReposts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PostEntityId",
                table: "Users",
                column: "PostEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PostEntityId1",
                table: "Users",
                column: "PostEntityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ImgId",
                table: "Posts",
                column: "ImgId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserLikesEntityId",
                table: "Posts",
                column: "UserLikesEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserRepostsEntityId",
                table: "Posts",
                column: "UserRepostsEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_ImgEntity",
                table: "Persons",
                column: "ImgEntity");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikes_UserId",
                table: "UserLikes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserReposts_UserId",
                table: "UserReposts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Imgs_ImgEntity",
                table: "Persons",
                column: "ImgEntity",
                principalTable: "Imgs",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserLikes_UserLikesEntityId",
                table: "Posts",
                column: "UserLikesEntityId",
                principalTable: "UserLikes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_UserReposts_UserRepostsEntityId",
                table: "Posts",
                column: "UserRepostsEntityId",
                principalTable: "UserReposts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_UserId",
                table: "Posts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Persons_PersonId",
                table: "Users",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Posts_PostEntityId",
                table: "Users",
                column: "PostEntityId",
                principalTable: "Posts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Posts_PostEntityId1",
                table: "Users",
                column: "PostEntityId1",
                principalTable: "Posts",
                principalColumn: "Id");
        }
    }
}
