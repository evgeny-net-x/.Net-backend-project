using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Backend6.Data.Migrations
{
    public partial class AddForums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forum_ForumCategories_CategoryId",
                table: "Forum");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopic_Forum_ForumId",
                table: "ForumTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forum",
                table: "Forum");

            migrationBuilder.RenameTable(
                name: "Forum",
                newName: "Forums");

            migrationBuilder.RenameIndex(
                name: "IX_Forum_CategoryId",
                table: "Forums",
                newName: "IX_Forums_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forums",
                table: "Forums",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forums_ForumCategories_CategoryId",
                table: "Forums",
                column: "CategoryId",
                principalTable: "ForumCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopic_Forums_ForumId",
                table: "ForumTopic",
                column: "ForumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Forums_ForumCategories_CategoryId",
                table: "Forums");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopic_Forums_ForumId",
                table: "ForumTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Forums",
                table: "Forums");

            migrationBuilder.RenameTable(
                name: "Forums",
                newName: "Forum");

            migrationBuilder.RenameIndex(
                name: "IX_Forums_CategoryId",
                table: "Forum",
                newName: "IX_Forum_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Forum",
                table: "Forum",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Forum_ForumCategories_CategoryId",
                table: "Forum",
                column: "CategoryId",
                principalTable: "ForumCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopic_Forum_ForumId",
                table: "ForumTopic",
                column: "ForumId",
                principalTable: "Forum",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
