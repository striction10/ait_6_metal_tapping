using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RUSAL.MetalTapping.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChemicalElem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChemicalElem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetalMark",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetalMark", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MetalMarkAnalysis",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetalMarkAnalysis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PotParametersGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotParametersGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PotState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reglament",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    DateStart = table.Column<DateTime>(type: "datetime", nullable: false),
                    DateStop = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reglament", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ScoopState",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScoopState", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TapTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShiftId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkGroup",
                columns: table => new
                {
                    WorkGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroup", x => x.WorkGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WeightOfMetal = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    MetalMarkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfOrder = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_MetalMark_MetalMarkId",
                        column: x => x.MetalMarkId,
                        principalTable: "MetalMark",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MetalMarkAnalysisValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChemicalElemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetalMarkAnalysisID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetalMarkAnalysisValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MetalMarkAnalysisValues_ChemicalElem_ChemicalElemId",
                        column: x => x.ChemicalElemId,
                        principalTable: "ChemicalElem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MetalMarkAnalysisValues_MetalMarkAnalysis_MetalMarkAnalysisID",
                        column: x => x.MetalMarkAnalysisID,
                        principalTable: "MetalMarkAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PotParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(10,0)", nullable: false),
                    PotParametersGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotParameters_PotParametersGroup_PotParametersGroupId",
                        column: x => x.PotParametersGroupId,
                        principalTable: "PotParametersGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pot_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pot_PotState_StateId",
                        column: x => x.StateId,
                        principalTable: "PotState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scoop",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scoop", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scoop_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scoop_ScoopState_StateId",
                        column: x => x.StateId,
                        principalTable: "ScoopState",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeginDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shift_WorkGroup_WorkGroupId",
                        column: x => x.WorkGroupId,
                        principalTable: "WorkGroup",
                        principalColumn: "WorkGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    WorkGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_WorkGroup_WorkGroupId",
                        column: x => x.WorkGroupId,
                        principalTable: "WorkGroup",
                        principalColumn: "WorkGroupId");
                });

            migrationBuilder.CreateTable(
                name: "ExternalData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotParametersGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DateOfReceipt = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalData_PotParametersGroup_PotParametersGroupId",
                        column: x => x.PotParametersGroupId,
                        principalTable: "PotParametersGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExternalData_Pot_PotId",
                        column: x => x.PotId,
                        principalTable: "Pot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PotReglament",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReglamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PotReglament", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PotReglament_Pot_PotId",
                        column: x => x.PotId,
                        principalTable: "Pot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PotReglament_Reglament_ReglamentId",
                        column: x => x.ReglamentId,
                        principalTable: "Reglament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TapTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    BuildingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ScoopId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TapTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TapTasks_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TapTasks_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TapTasks_Scoop_ScoopId",
                        column: x => x.ScoopId,
                        principalTable: "Scoop",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleMembers",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleMembers", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoleMembers_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleMembers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkGroupMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroupMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkGroupMembers_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkGroupMembers_WorkGroup_WorkGroupId",
                        column: x => x.WorkGroupId,
                        principalTable: "WorkGroup",
                        principalColumn: "WorkGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Deviation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TargetMetalLevel = table.Column<decimal>(type: "numeric(4,0)", nullable: false),
                    PotReglamentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deviation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Deviation_PotReglament_PotReglamentId",
                        column: x => x.PotReglamentId,
                        principalTable: "PotReglament",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TapTaskPot",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TapTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MetalMarkAnalysisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PotMetalWeight = table.Column<decimal>(type: "numeric(10,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TapTaskPot", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TapTaskPot_MetalMarkAnalysis_MetalMarkAnalysisId",
                        column: x => x.MetalMarkAnalysisId,
                        principalTable: "MetalMarkAnalysis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TapTaskPot_Pot_PotId",
                        column: x => x.PotId,
                        principalTable: "Pot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TapTaskPot_TapTasks_TapTaskId",
                        column: x => x.TapTaskId,
                        principalTable: "TapTasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviationValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviationID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Value = table.Column<decimal>(type: "numeric(4,0)", nullable: false),
                    CastingRatio = table.Column<decimal>(type: "numeric(4,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviationValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviationValues_Deviation_DeviationID",
                        column: x => x.DeviationID,
                        principalTable: "Deviation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deviation_PotReglamentId",
                table: "Deviation",
                column: "PotReglamentId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviationValues_DeviationID",
                table: "DeviationValues",
                column: "DeviationID");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalData_PotId",
                table: "ExternalData",
                column: "PotId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalData_PotParametersGroupId",
                table: "ExternalData",
                column: "PotParametersGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MetalMarkAnalysisValues_ChemicalElemId",
                table: "MetalMarkAnalysisValues",
                column: "ChemicalElemId");

            migrationBuilder.CreateIndex(
                name: "IX_MetalMarkAnalysisValues_MetalMarkAnalysisID",
                table: "MetalMarkAnalysisValues",
                column: "MetalMarkAnalysisID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MetalMarkId",
                table: "Order",
                column: "MetalMarkId");

            migrationBuilder.CreateIndex(
                name: "IX_Pot_BuildingId",
                table: "Pot",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Pot_StateId",
                table: "Pot",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PotParameters_PotParametersGroupId",
                table: "PotParameters",
                column: "PotParametersGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PotReglament_PotId",
                table: "PotReglament",
                column: "PotId");

            migrationBuilder.CreateIndex(
                name: "IX_PotReglament_ReglamentId",
                table: "PotReglament",
                column: "ReglamentId");

            migrationBuilder.CreateIndex(
                name: "IX_Scoop_BuildingId",
                table: "Scoop",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Scoop_StateId",
                table: "Scoop",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Shift_WorkGroupId",
                table: "Shift",
                column: "WorkGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTaskPot_MetalMarkAnalysisId",
                table: "TapTaskPot",
                column: "MetalMarkAnalysisId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTaskPot_PotId",
                table: "TapTaskPot",
                column: "PotId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTaskPot_TapTaskId",
                table: "TapTaskPot",
                column: "TapTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTasks_BuildingId",
                table: "TapTasks",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTasks_OrderId",
                table: "TapTasks",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_TapTasks_ScoopId",
                table: "TapTasks",
                column: "ScoopId");

            migrationBuilder.CreateIndex(
                name: "IX_User_WorkGroupId",
                table: "User",
                column: "WorkGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMembers_RoleId",
                table: "UserRoleMembers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupMembers_UserId",
                table: "WorkGroupMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupMembers_WorkGroupId",
                table: "WorkGroupMembers",
                column: "WorkGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviationValues");

            migrationBuilder.DropTable(
                name: "ExternalData");

            migrationBuilder.DropTable(
                name: "MetalMarkAnalysisValues");

            migrationBuilder.DropTable(
                name: "PotParameters");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "TapTaskPot");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "UserRoleMembers");

            migrationBuilder.DropTable(
                name: "WorkGroupMembers");

            migrationBuilder.DropTable(
                name: "Deviation");

            migrationBuilder.DropTable(
                name: "ChemicalElem");

            migrationBuilder.DropTable(
                name: "PotParametersGroup");

            migrationBuilder.DropTable(
                name: "MetalMarkAnalysis");

            migrationBuilder.DropTable(
                name: "TapTasks");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PotReglament");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Scoop");

            migrationBuilder.DropTable(
                name: "WorkGroup");

            migrationBuilder.DropTable(
                name: "Pot");

            migrationBuilder.DropTable(
                name: "Reglament");

            migrationBuilder.DropTable(
                name: "MetalMark");

            migrationBuilder.DropTable(
                name: "ScoopState");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "PotState");
        }
    }
}
