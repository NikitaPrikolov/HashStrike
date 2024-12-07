using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HashStrike.Api.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRequestTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Task = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MinLineLength = table.Column<int>(type: "int", nullable: false),
                    MaxLineLength = table.Column<int>(type: "int", nullable: false),
                    HasCapitalLetters = table.Column<bool>(type: "bit", nullable: false),
                    HasSmallLetters = table.Column<bool>(type: "bit", nullable: false),
                    HasNumbers = table.Column<bool>(type: "bit", nullable: false),
                    HasSpecialCharacters = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hosts");

            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}
