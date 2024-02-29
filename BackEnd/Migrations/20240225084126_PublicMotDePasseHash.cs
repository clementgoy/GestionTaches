using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class PublicMotDePasseHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEmploye",
                table: "Assignations");

            migrationBuilder.DropColumn(
                name: "IdTache",
                table: "Assignations");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Assignations");

            migrationBuilder.AddColumn<string>(
                name: "MotDePasseHash",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReinitialiserMDPJeton",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReinitialiserMDPJetonExpiration",
                table: "Employes",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotDePasseHash",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "ReinitialiserMDPJeton",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "ReinitialiserMDPJetonExpiration",
                table: "Employes");

            migrationBuilder.AddColumn<int>(
                name: "IdEmploye",
                table: "Assignations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdTache",
                table: "Assignations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Assignations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
