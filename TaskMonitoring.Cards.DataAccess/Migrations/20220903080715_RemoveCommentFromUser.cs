﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskMonitoring.Cards.DataAccess.Migrations
{
    public partial class RemoveCommentFromUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "Comments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
