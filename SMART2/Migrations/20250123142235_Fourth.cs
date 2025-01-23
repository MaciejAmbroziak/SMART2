using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SMART2.Migrations
{
    /// <inheritdoc />
    public partial class Fourth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionFacilities_EquipmentContracts_EquipmentContractId",
                table: "ProductionFacilities");

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentContractId",
                table: "ProductionFacilities",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionFacilities_EquipmentContracts_EquipmentContractId",
                table: "ProductionFacilities",
                column: "EquipmentContractId",
                principalTable: "EquipmentContracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductionFacilities_EquipmentContracts_EquipmentContractId",
                table: "ProductionFacilities");

            migrationBuilder.AlterColumn<int>(
                name: "EquipmentContractId",
                table: "ProductionFacilities",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductionFacilities_EquipmentContracts_EquipmentContractId",
                table: "ProductionFacilities",
                column: "EquipmentContractId",
                principalTable: "EquipmentContracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
