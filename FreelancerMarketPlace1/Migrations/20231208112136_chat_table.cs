using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FreelancerMarketPlace1.Migrations
{
    /// <inheritdoc />
    public partial class chat_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "MessageDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "MessageDetails",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "MessageStatus",
                table: "Chats",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageDate",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "MessageDetails",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "MessageStatus",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Sender",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "Chats");
        }
    }
}
