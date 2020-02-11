using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreDuiWebApi.Data.Migrations
{
    public partial class UpdatedUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "DbUsers",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "AccountEnabled",
                table: "DbUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "ConfirmEmailToken",
                table: "DbUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ConfirmEmailTokenExpiresAt",
                table: "DbUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "DbUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Hash",
                table: "DbUsers",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RefreshToken",
                table: "DbUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiresAt",
                table: "DbUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Salt",
                table: "DbUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DbUsers_EmailAddress",
                table: "DbUsers",
                column: "EmailAddress",
                unique: true,
                filter: "[EmailAddress] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DbUsers_EmailAddress",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "AccountEnabled",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "ConfirmEmailToken",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "ConfirmEmailTokenExpiresAt",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "Hash",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresAt",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "Salt",
                table: "DbUsers");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderId",
                table: "DbUsers",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
