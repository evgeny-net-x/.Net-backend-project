using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Backend6.Data.Migrations
{
    public partial class AddForumTopics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessage_ForumTopic_TopicId",
                table: "ForumMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopic_AspNetUsers_CreatorId",
                table: "ForumTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopic_Forums_ForumId",
                table: "ForumTopic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumTopic",
                table: "ForumTopic");

            migrationBuilder.RenameTable(
                name: "ForumTopic",
                newName: "ForumTopics");

            migrationBuilder.RenameIndex(
                name: "IX_ForumTopic_ForumId",
                table: "ForumTopics",
                newName: "IX_ForumTopics_ForumId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumTopic_CreatorId",
                table: "ForumTopics",
                newName: "IX_ForumTopics_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumTopics",
                table: "ForumTopics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessage_ForumTopics_TopicId",
                table: "ForumMessage",
                column: "TopicId",
                principalTable: "ForumTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopics_AspNetUsers_CreatorId",
                table: "ForumTopics",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopics_Forums_ForumId",
                table: "ForumTopics",
                column: "ForumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessage_ForumTopics_TopicId",
                table: "ForumMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopics_AspNetUsers_CreatorId",
                table: "ForumTopics");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumTopics_Forums_ForumId",
                table: "ForumTopics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumTopics",
                table: "ForumTopics");

            migrationBuilder.RenameTable(
                name: "ForumTopics",
                newName: "ForumTopic");

            migrationBuilder.RenameIndex(
                name: "IX_ForumTopics_ForumId",
                table: "ForumTopic",
                newName: "IX_ForumTopic_ForumId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumTopics_CreatorId",
                table: "ForumTopic",
                newName: "IX_ForumTopic_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumTopic",
                table: "ForumTopic",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessage_ForumTopic_TopicId",
                table: "ForumMessage",
                column: "TopicId",
                principalTable: "ForumTopic",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopic_AspNetUsers_CreatorId",
                table: "ForumTopic",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumTopic_Forums_ForumId",
                table: "ForumTopic",
                column: "ForumId",
                principalTable: "Forums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
