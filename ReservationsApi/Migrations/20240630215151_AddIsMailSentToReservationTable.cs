using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReservationsApi.Migrations
{
    /// <inheritdoc />
    public partial class AddIsMailSentToReservationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMailSent",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMailSent",
                table: "Reservations");
        }
    }
}
