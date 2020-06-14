using Microsoft.EntityFrameworkCore.Migrations;

namespace WineForum.Data.Migrations
{
    public partial class final2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAdmin",
                table: "AspNetUsers",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AspNetUsers",
                newName: "IsAdmin");
        }
    }
}
