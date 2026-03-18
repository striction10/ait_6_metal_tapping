using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixUserRoleMembersId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMembers",
                table: "UserRoleMembers");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMembers_UserId",
                table: "UserRoleMembers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRoleMembers");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MetalMarkAnalysis",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "RoundCalculatedTask",
                table: "CalculatedTasks",
                type: "numeric(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CalculatedTask",
                table: "CalculatedTasks",
                type: "numeric(18,4)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMembers",
                table: "UserRoleMembers",
                columns: new[] { "UserId", "RoleId" });
        }
    }
}
