using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculatedTasksTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "DeviationValues",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,0)");

            migrationBuilder.AlterColumn<int>(
                name: "CastingRatio",
                table: "DeviationValues",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(4,0)");

            migrationBuilder.CreateTable(
                name: "CalculatedTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CalculatedTask = table.Column<decimal>(type: "numeric(18,4)", nullable: false),
                    RoundCalculatedTask = table.Column<decimal>(type: "numeric(18,4)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculatedTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CalculatedTasks_Pot_PotId",
                        column: x => x.PotId,
                        principalTable: "Pot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CalculatedTasks_PotId",
                table: "CalculatedTasks",
                column: "PotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculatedTasks");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Pot");

            migrationBuilder.DropColumn(
                name: "ActualMetalLevel",
                table: "Deviation");

            migrationBuilder.DropColumn(
                name: "IsValid",
                table: "Deviation");

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "DeviationValues",
                type: "numeric(4,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "CastingRatio",
                table: "DeviationValues",
                type: "numeric(4,0)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Deviation",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");
        }
    }
}
