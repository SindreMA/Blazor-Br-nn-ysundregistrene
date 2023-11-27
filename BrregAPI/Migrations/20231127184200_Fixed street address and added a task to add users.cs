using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrregAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fixedstreetaddressandaddedatasktoaddusers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Rolle",
                table: "Personer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RolleNavn",
                table: "Personer",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GateAdresse",
                table: "Adresser",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rolle",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "RolleNavn",
                table: "Personer");

            migrationBuilder.DropColumn(
                name: "GateAdresse",
                table: "Adresser");
        }
    }
}
