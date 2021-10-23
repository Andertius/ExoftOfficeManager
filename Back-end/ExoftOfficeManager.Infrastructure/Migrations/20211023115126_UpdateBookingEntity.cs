using Microsoft.EntityFrameworkCore.Migrations;

namespace ExoftOfficeManager.Infrastructure.Migrations
{
    public partial class UpdateBookingEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DayNumber",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayNumber",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bookings");
        }
    }
}
