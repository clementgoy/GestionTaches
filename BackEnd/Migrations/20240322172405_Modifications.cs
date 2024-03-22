using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class Modifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HashedIdEmploye",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "HashedIdEmploye",
                table: "Assignations");

            migrationBuilder.DropColumn(
                name: "HashedIdTache",
                table: "Assignations");

            migrationBuilder.AlterColumn<string>(
                name: "Statut",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Pole",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "IdEmploye",
                table: "Conges",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdEmploye",
                table: "Conges");

            migrationBuilder.DropColumn(
                name: "IdEmploye",
                table: "Assignations");

            migrationBuilder.DropColumn(
                name: "IdTache",
                table: "Assignations");

            migrationBuilder.AlterColumn<int>(
                name: "Statut",
                table: "Employes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Pole",
                table: "Employes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "HashedIdEmploye",
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
    }
}
