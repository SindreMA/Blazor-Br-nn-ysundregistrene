using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrregAPI.Migrations
{
    /// <inheritdoc />
    public partial class Madeintoptinonal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InstitusjonellSektorkode",
                table: "Firmaer",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "InstitusjonellSektorkode",
                table: "Firmaer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
