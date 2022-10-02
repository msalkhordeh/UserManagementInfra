using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UM.DataAccess.Migrations
{
    public partial class Update_User_AddPasswordAndRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PasswordSalt",
                schema: "Identity",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<byte>(
                name: "Role",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: true,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                schema: "Identity",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Role",
                schema: "Identity",
                table: "Users");
        }
    }
}
