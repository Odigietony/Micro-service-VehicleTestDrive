using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomersApi.Migrations
{
    /// <inheritdoc />
    public partial class AddVehicleIdToCustomerModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Vehicles_VehicleId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Vehicles_VehicleId",
                table: "Customers",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Vehicles_VehicleId",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "VehicleId",
                table: "Customers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Vehicles_VehicleId",
                table: "Customers",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id");
        }
    }
}
