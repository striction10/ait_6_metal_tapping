using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTasksRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Shift_ShiftId",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TapTasks_TapTaskId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_ShiftId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_TapTaskId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LeadTime",
                table: "Tasks");
        }
    }
}
