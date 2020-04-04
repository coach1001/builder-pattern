using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreDuiWebApi.Data.Migrations
{
    public partial class RoleToUserUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbUserRoles_DbUsers_DbUserId",
                table: "DbUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "DbUserRoles",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DbUserId",
                table: "DbUserRoles",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbUserRoles_Role_DbUserId",
                table: "DbUserRoles",
                columns: new[] { "Role", "DbUserId" },
                unique: true,
                filter: "[Role] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_DbUserRoles_DbUsers_DbUserId",
                table: "DbUserRoles",
                column: "DbUserId",
                principalTable: "DbUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DbUserRoles_DbUsers_DbUserId",
                table: "DbUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_DbUserRoles_Role_DbUserId",
                table: "DbUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Role",
                table: "DbUserRoles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DbUserId",
                table: "DbUserRoles",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_DbUserRoles_DbUsers_DbUserId",
                table: "DbUserRoles",
                column: "DbUserId",
                principalTable: "DbUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
