using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreDuiWebApi.Data.Migrations
{
    public partial class AddResetPasswordTokenToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ResetPasswordToken",
                table: "DbUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "ResetPasswordTokenExpiresAt",
                table: "DbUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResetPasswordToken",
                table: "DbUsers");

            migrationBuilder.DropColumn(
                name: "ResetPasswordTokenExpiresAt",
                table: "DbUsers");
        }
    }
}
