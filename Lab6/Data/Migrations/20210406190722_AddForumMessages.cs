using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Backend6.Data.Migrations
{
    public partial class AddForumMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessage_AspNetUsers_CreatorId",
                table: "ForumMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessage_ForumTopics_TopicId",
                table: "ForumMessage");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessageAttachment_ForumMessage_MessageId",
                table: "ForumMessageAttachment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumMessage",
                table: "ForumMessage");

            migrationBuilder.RenameTable(
                name: "ForumMessage",
                newName: "ForumMessages");

            migrationBuilder.RenameIndex(
                name: "IX_ForumMessage_TopicId",
                table: "ForumMessages",
                newName: "IX_ForumMessages_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumMessage_CreatorId",
                table: "ForumMessages",
                newName: "IX_ForumMessages_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumMessages",
                table: "ForumMessages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessageAttachment_ForumMessages_MessageId",
                table: "ForumMessageAttachment",
                column: "MessageId",
                principalTable: "ForumMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessages_AspNetUsers_CreatorId",
                table: "ForumMessages",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessages_ForumTopics_TopicId",
                table: "ForumMessages",
                column: "TopicId",
                principalTable: "ForumTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessageAttachment_ForumMessages_MessageId",
                table: "ForumMessageAttachment");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessages_AspNetUsers_CreatorId",
                table: "ForumMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_ForumMessages_ForumTopics_TopicId",
                table: "ForumMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ForumMessages",
                table: "ForumMessages");

            migrationBuilder.RenameTable(
                name: "ForumMessages",
                newName: "ForumMessage");

            migrationBuilder.RenameIndex(
                name: "IX_ForumMessages_TopicId",
                table: "ForumMessage",
                newName: "IX_ForumMessage_TopicId");

            migrationBuilder.RenameIndex(
                name: "IX_ForumMessages_CreatorId",
                table: "ForumMessage",
                newName: "IX_ForumMessage_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ForumMessage",
                table: "ForumMessage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessage_AspNetUsers_CreatorId",
                table: "ForumMessage",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessage_ForumTopics_TopicId",
                table: "ForumMessage",
                column: "TopicId",
                principalTable: "ForumTopics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ForumMessageAttachment_ForumMessage_MessageId",
                table: "ForumMessageAttachment",
                column: "MessageId",
                principalTable: "ForumMessage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
