using Microsoft.EntityFrameworkCore.Migrations;

namespace Panda.Data.SqlServer.Migrations
{
    public partial class AddDeletedFieldToPostTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "Posts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Posts");
        }
    }
}
