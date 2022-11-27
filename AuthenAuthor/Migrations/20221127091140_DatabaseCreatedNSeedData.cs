using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthenAuthor.Migrations
{
    public partial class DatabaseCreatedNSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "Age", "DateOfBirth", "Email", "Gender", "Password", "UserName" },
                values: new object[] { 1, "Hanoi", 23, new DateTime(1999, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "tuananh@gmail.com", true, "Tuananh123.", "tuananh" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
