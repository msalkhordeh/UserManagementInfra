using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UM.DataAccess.Migrations
{
    public partial class Add_UrlResolvers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Log");

            migrationBuilder.CreateTable(
                name: "UrlResolvers",
                schema: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Host = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    StatusCode = table.Column<int>(type: "int", nullable: false),
                    BodyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserAgent = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Verb = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Ip = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    ExecuteTime = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrlResolvers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrlResolvers",
                schema: "Log");
        }
    }
}
