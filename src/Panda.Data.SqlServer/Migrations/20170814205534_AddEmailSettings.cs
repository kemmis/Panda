using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Panda.Data.SqlServer.Migrations
{
    public partial class AddEmailSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "SendCommentEmail",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SmtpHost",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpPassword",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpPort",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SmtpPrefix",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SmtpUseSsl",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SmtpUsername",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SendCommentEmail",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SmtpHost",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SmtpPassword",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SmtpPort",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SmtpPrefix",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SmtpUseSsl",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "SmtpUsername",
                table: "Blogs");
        }
    }
}
