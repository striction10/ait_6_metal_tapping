using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddShiftsToBuildingForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BuildingId",
                table: "Shift",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Shift_BuildingId",
                table: "Shift",
                column: "BuildingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Shift_Building_BuildingId",
                table: "Shift",
                column: "BuildingId",
                principalTable: "Building",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Shift_Building_BuildingId",
                table: "Shift");

            migrationBuilder.DropIndex(
                name: "IX_Shift_BuildingId",
                table: "Shift");

            migrationBuilder.DropColumn(
                name: "BuildingId",
                table: "Shift");
        }
    }
}
