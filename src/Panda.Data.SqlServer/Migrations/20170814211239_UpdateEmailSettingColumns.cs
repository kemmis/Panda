using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Panda.Data.SqlServer.Migrations
{
    public partial class UpdateEmailSettingColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SmtpPrefix",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "EmailPrefix",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailPrefix",
                table: "Blogs");

            migrationBuilder.AddColumn<string>(
                name: "SmtpPrefix",
                table: "Blogs",
                nullable: true);
        }
    }
}
