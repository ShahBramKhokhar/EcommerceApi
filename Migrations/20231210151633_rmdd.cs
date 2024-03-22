using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebRexErpAPI.Migrations
{
    public partial class rmdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblUser_UserTypes_UserTypeId",
                table: "tblUser");

            migrationBuilder.DropIndex(
                name: "IX_tblUser_UserTypeId",
                table: "tblUser");

            migrationBuilder.DropColumn(
                name: "UserTypeId",
                table: "tblUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserTypeId",
                table: "tblUser",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tblUser_UserTypeId",
                table: "tblUser",
                column: "UserTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblUser_UserTypes_UserTypeId",
                table: "tblUser",
                column: "UserTypeId",
                principalTable: "UserTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
