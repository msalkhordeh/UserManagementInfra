using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UM.DataAccess.Migrations
{
    public partial class FixDefaultValuesToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: true,
                defaultValue: (byte)1,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)1);

            migrationBuilder.AlterColumn<byte>(
                name: "Gender",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: true,
                defaultValue: (byte)2,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)2);

            migrationBuilder.AlterColumn<byte>(
                name: "Type",
                schema: "Identity",
                table: "Phones",
                type: "tinyint",
                nullable: true,
                defaultValue: (byte)5,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)5);

            migrationBuilder.AlterColumn<byte>(
                name: "Type",
                schema: "Identity",
                table: "Addresses",
                type: "tinyint",
                nullable: true,
                defaultValue: (byte)5,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldDefaultValue: (byte)5);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Status",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)1,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true,
                oldDefaultValue: (byte)1);

            migrationBuilder.AlterColumn<byte>(
                name: "Gender",
                schema: "Identity",
                table: "Users",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)2,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true,
                oldDefaultValue: (byte)2);

            migrationBuilder.AlterColumn<byte>(
                name: "Type",
                schema: "Identity",
                table: "Phones",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)5,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true,
                oldDefaultValue: (byte)5);

            migrationBuilder.AlterColumn<byte>(
                name: "Type",
                schema: "Identity",
                table: "Addresses",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)5,
                oldClrType: typeof(byte),
                oldType: "tinyint",
                oldNullable: true,
                oldDefaultValue: (byte)5);
        }
    }
}
