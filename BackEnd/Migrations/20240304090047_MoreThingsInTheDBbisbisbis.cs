using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MoreThingsInTheDBbisbisbis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEmploye",
                table: "Conges");

            migrationBuilder.AddColumn<string>(
                name: "HashedIdEmploye",
                table: "Conges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Motif",
                table: "Conges",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HashedIdEmploye",
                table: "Assignations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HashedIdTache",
                table: "Assignations",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedIdEmploye",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "Motif",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "HashedIdEmploye",
                table: "Assignations");

            migrationBuilder.DropColumn(
                name: "HashedIdTache",
                table: "Assignations");

            migrationBuilder.AddColumn<int>(
                name: "IdEmploye",
                table: "Conges",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
