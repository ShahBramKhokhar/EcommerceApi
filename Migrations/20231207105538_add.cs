using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRexErpAPI.Migrations
{
    public partial class add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserTypeId",
                table: "UserTypes",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserTypes",
                newName: "UserTypeId");
        }
    }
}
