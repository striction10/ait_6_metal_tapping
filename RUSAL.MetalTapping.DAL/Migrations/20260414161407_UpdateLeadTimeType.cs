using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLeadTimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "LeadTime",
                table: "Tasks",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
