using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreDuiWebApi.Data.Migrations
{
    public partial class AddedRolesToUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbUserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    DbUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbUserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbUserRoles_DbUsers_DbUserId",
                        column: x => x.DbUserId,
                        principalTable: "DbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbUserRoles_DbUserId",
                table: "DbUserRoles",
                column: "DbUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbUserRoles");
        }
    }
}
