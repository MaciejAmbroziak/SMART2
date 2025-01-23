using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMART2.Migrations
{
    /// <inheritdoc />
    public partial class Third : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EquipmentContracts_ProductionFacilities_ProductionFacilityId",
                table: "EquipmentContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcessEquipmentEquipmentContracts_ProcessEquipments_ProcessEquipmentId",
                table: "ProcessEquipmentEquipmentContracts");

            migrationBuilder.DropIndex(
                name: "IX_EquipmentContracts_ProductionFacilityId",
                table: "EquipmentContracts");

            migrationBuilder.DropColumn(
                name: "ContractQuantity",
                table: "ProcessEquipments");

            migrationBuilder.DropColumn(
                name: "ProductionFacilityId",
                table: "EquipmentContracts");

            migrationBuilder.RenameColumn(
                name: "ProcessEquipmentId",
                table: "ProcessEquipmentEquipmentContracts",
                newName: "ProcessEquipmentsId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessEquipmentEquipmentContracts_ProcessEquipmentId",
                table: "ProcessEquipmentEquipmentContracts",
                newName: "IX_ProcessEquipmentEquipmentContracts_ProcessEquipmentsId");

            migrationBuilder.AddColumn<int>(
                name: "EquipmentContractId",
                table: "ProductionFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductionFacilities_EquipmentContractId",
                table: "ProductionFacilities",
                column: "EquipmentContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessEquipmentEquipmentContracts_ProcessEquipments_ProcessEquipmentsId",
                table: "ProcessEquipmentEquipmentContracts",
                column: "ProcessEquipmentsId",
                principalTable: "ProcessEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionFacilities_EquipmentContracts_EquipmentContractId",
                table: "ProductionFacilities",
                column: "EquipmentContractId",
                principalTable: "EquipmentContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProcessEquipmentEquipmentContracts_ProcessEquipments_ProcessEquipmentsId",
                table: "ProcessEquipmentEquipmentContracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductionFacilities_EquipmentContracts_EquipmentContractId",
                table: "ProductionFacilities");

            migrationBuilder.DropIndex(
                name: "IX_ProductionFacilities_EquipmentContractId",
                table: "ProductionFacilities");

            migrationBuilder.DropColumn(
                name: "EquipmentContractId",
                table: "ProductionFacilities");

            migrationBuilder.RenameColumn(
                name: "ProcessEquipmentsId",
                table: "ProcessEquipmentEquipmentContracts",
                newName: "ProcessEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ProcessEquipmentEquipmentContracts_ProcessEquipmentsId",
                table: "ProcessEquipmentEquipmentContracts",
                newName: "IX_ProcessEquipmentEquipmentContracts_ProcessEquipmentId");

            migrationBuilder.AddColumn<int>(
                name: "ContractQuantity",
                table: "ProcessEquipments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductionFacilityId",
                table: "EquipmentContracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EquipmentContracts_ProductionFacilityId",
                table: "EquipmentContracts",
                column: "ProductionFacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_EquipmentContracts_ProductionFacilities_ProductionFacilityId",
                table: "EquipmentContracts",
                column: "ProductionFacilityId",
                principalTable: "ProductionFacilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcessEquipmentEquipmentContracts_ProcessEquipments_ProcessEquipmentId",
                table: "ProcessEquipmentEquipmentContracts",
                column: "ProcessEquipmentId",
                principalTable: "ProcessEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
