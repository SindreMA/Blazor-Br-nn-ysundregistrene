using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrregAPI.Migrations
{
    /// <inheritdoc />
    public partial class Fixedwrongtypeonisadmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Custom code
            //Normally would migrate, but since this is new, we can just drop and add

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers"
            );
            
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //Custom code
            //Should probably migrate here since we want to protect going back, have time limit

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers"
            );

            migrationBuilder.AddColumn<string>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "text",
                nullable: false,
                defaultValue: ""
            );
        }
    }
}
