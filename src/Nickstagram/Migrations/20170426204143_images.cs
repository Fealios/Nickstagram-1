using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Nickstagram.Migrations
{
    public partial class images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Posts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PostUserId",
                table: "Posts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posts_PostUserId",
                table: "Posts",
                column: "PostUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_AspNetUsers_PostUserId",
                table: "Posts",
                column: "PostUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Posts_AspNetUsers_PostUserId",
                table: "Posts");

            migrationBuilder.DropIndex(
                name: "IX_Posts_PostUserId",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PostUserId",
                table: "Posts");
        }
    }
}
