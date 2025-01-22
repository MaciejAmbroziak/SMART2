using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMART2.Migrations
{
    /// <inheritdoc />
    public partial class First : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProcessEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Area = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEquipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductionFacilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StandardArea = table.Column<double>(type: "float", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Occupied = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductionFacilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EquipmentContracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductionFacilityId = table.Column<int>(type: "int", nullable: false),
                    TotalEquipmentUnits = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipmentContracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EquipmentContracts_ProductionFacilities_ProductionFacilityId",
                        column: x => x.ProductionFacilityId,
                        principalTable: "ProductionFacilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProcessEquipmentEquipmentContracts",
                columns: table => new
                {
                    EquipmentContractsId = table.Column<int>(type: "int", nullable: false),
                    ProcessEquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessEquipmentEquipmentContracts", x => new { x.EquipmentContractsId, x.ProcessEquipmentId });
                    table.ForeignKey(
                        name: "FK_ProcessEquipmentEquipmentContracts_EquipmentContracts_EquipmentContractsId",
                        column: x => x.EquipmentContractsId,
                        principalTable: "EquipmentContracts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessEquipmentEquipmentContracts_ProcessEquipments_ProcessEquipmentId",
                        column: x => x.ProcessEquipmentId,
                        principalTable: "ProcessEquipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentContracts_ProductionFacilityId",
                table: "EquipmentContracts",
                column: "ProductionFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessEquipmentEquipmentContracts_ProcessEquipmentId",
                table: "ProcessEquipmentEquipmentContracts",
                column: "ProcessEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessEquipments_Code",
                table: "ProcessEquipments",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionFacilities_Code",
                table: "ProductionFacilities",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessEquipmentEquipmentContracts");

            migrationBuilder.DropTable(
                name: "EquipmentContracts");

            migrationBuilder.DropTable(
                name: "ProcessEquipments");

            migrationBuilder.DropTable(
                name: "ProductionFacilities");
        }
    }
}
