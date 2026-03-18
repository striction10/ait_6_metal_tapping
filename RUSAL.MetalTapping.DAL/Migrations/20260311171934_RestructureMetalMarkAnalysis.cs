using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RestructureMetalMarkAnalysis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_MetalMarkAnalysis_MetalMarkId",
                table: "MetalMarkAnalysis",
                column: "MetalMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_MetalMarkAnalysis_PotId",
                table: "MetalMarkAnalysis",
                column: "PotId");

            migrationBuilder.AddForeignKey(
                name: "FK_MetalMarkAnalysis_MetalMark_MetalMarkId",
                table: "MetalMarkAnalysis",
                column: "MetalMarkId",
                principalTable: "MetalMark",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MetalMarkAnalysis_Pot_PotId",
                table: "MetalMarkAnalysis",
                column: "PotId",
                principalTable: "Pot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MetalMarkAnalysis_MetalMark_MetalMarkId",
                table: "MetalMarkAnalysis");

            migrationBuilder.DropForeignKey(
                name: "FK_MetalMarkAnalysis_Pot_PotId",
                table: "MetalMarkAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_MetalMarkAnalysis_MetalMarkId",
                table: "MetalMarkAnalysis");

            migrationBuilder.DropIndex(
                name: "IX_MetalMarkAnalysis_PotId",
                table: "MetalMarkAnalysis");

            migrationBuilder.DropColumn(
                name: "MetalMarkId",
                table: "MetalMarkAnalysis");

            migrationBuilder.DropColumn(
                name: "PotId",
                table: "MetalMarkAnalysis");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CalculatedTasks");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfReceipt",
                table: "MetalMarkAnalysisValues",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
