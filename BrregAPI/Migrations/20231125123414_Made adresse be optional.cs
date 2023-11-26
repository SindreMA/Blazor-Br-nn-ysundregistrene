using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BrregAPI.Migrations
{
    /// <inheritdoc />
    public partial class Madeadressebeoptional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firmaer_Adresser_ForretningsadresseId",
                table: "Firmaer");

            migrationBuilder.DropForeignKey(
                name: "FK_Firmaer_Adresser_PostadresseId",
                table: "Firmaer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Stiftelsesdato",
                table: "Firmaer",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistreringsdatoEnhetsregisteret",
                table: "Firmaer",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<int>(
                name: "PostadresseId",
                table: "Firmaer",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "ForretningsadresseId",
                table: "Firmaer",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Firmaer_Adresser_ForretningsadresseId",
                table: "Firmaer",
                column: "ForretningsadresseId",
                principalTable: "Adresser",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Firmaer_Adresser_PostadresseId",
                table: "Firmaer",
                column: "PostadresseId",
                principalTable: "Adresser",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Firmaer_Adresser_ForretningsadresseId",
                table: "Firmaer");

            migrationBuilder.DropForeignKey(
                name: "FK_Firmaer_Adresser_PostadresseId",
                table: "Firmaer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Stiftelsesdato",
                table: "Firmaer",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegistreringsdatoEnhetsregisteret",
                table: "Firmaer",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "PostadresseId",
                table: "Firmaer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ForretningsadresseId",
                table: "Firmaer",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Firmaer_Adresser_ForretningsadresseId",
                table: "Firmaer",
                column: "ForretningsadresseId",
                principalTable: "Adresser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Firmaer_Adresser_PostadresseId",
                table: "Firmaer",
                column: "PostadresseId",
                principalTable: "Adresser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
