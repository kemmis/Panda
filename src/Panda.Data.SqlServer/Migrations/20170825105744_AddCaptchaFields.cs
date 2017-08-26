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
                name: "CaptchaKey",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CaptchaSecret",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "UseReCaptcha",
                table: "Blogs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaptchaKey",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "CaptchaSecret",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UseReCaptcha",
                table: "Blogs");
        }
    }
}
