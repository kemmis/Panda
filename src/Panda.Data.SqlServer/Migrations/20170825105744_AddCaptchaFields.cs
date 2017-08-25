using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Panda.Data.SqlServer.Migrations
{
    public partial class AddCaptchaFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "captchaKey",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "captchaSecret",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "useReCaptcha",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "captchaKey",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "captchaSecret",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "useReCaptcha",
                table: "Blogs");
        }
    }
}
