using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SkillsService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkillCatalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillCatalogId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserSkills_SkillCatalog_SkillCatalogId",
                        column: x => x.SkillCatalogId,
                        principalTable: "SkillCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SkillCatalog",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0001-0000-0000-000000000000"), "Web Design" },
                    { new Guid("a1b2c3d4-0002-0000-0000-000000000000"), "Graphic Design" },
                    { new Guid("a1b2c3d4-0003-0000-0000-000000000000"), "UI Design" },
                    { new Guid("a1b2c3d4-0004-0000-0000-000000000000"), "UX Design" },
                    { new Guid("a1b2c3d4-0005-0000-0000-000000000000"), "Mobile App Design" },
                    { new Guid("a1b2c3d4-0006-0000-0000-000000000000"), "Illustration" },
                    { new Guid("a1b2c3d4-0007-0000-0000-000000000000"), "Animation" },
                    { new Guid("a1b2c3d4-0008-0000-0000-000000000000"), "Branding" },
                    { new Guid("a1b2c3d4-0009-0000-0000-000000000000"), "Product Design" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSkills_SkillCatalogId",
                table: "UserSkills",
                column: "SkillCatalogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.DropTable(
                name: "SkillCatalog");
        }
    }
}
