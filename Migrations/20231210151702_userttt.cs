using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRexErpAPI.Migrations
{
    public partial class userttt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "tblUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "tblUser");
        }
    }
}
