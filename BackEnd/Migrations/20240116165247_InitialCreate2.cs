using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "echeance",
                table: "Taches",
                newName: "Echeance");

            migrationBuilder.RenameColumn(
                name: "duree",
                table: "Taches",
                newName: "Duree");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Taches",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Taches",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "idEmploye",
                table: "Conges",
                newName: "IdEmploye");

            migrationBuilder.RenameColumn(
                name: "duree",
                table: "Conges",
                newName: "Duree");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Conges",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Conges",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "message",
                table: "Assignations",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "idTache",
                table: "Assignations",
                newName: "IdTache");

            migrationBuilder.RenameColumn(
                name: "idEmploye",
                table: "Assignations",
                newName: "IdEmploye");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Assignations",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Pole",
                table: "Employes",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<string>(
                name: "MotDePasse",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Travail",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotDePasse",
                table: "Employes");

            migrationBuilder.DropColumn(
                name: "Travail",
                table: "Employes");

            migrationBuilder.RenameColumn(
                name: "Echeance",
                table: "Taches",
                newName: "echeance");

            migrationBuilder.RenameColumn(
                name: "Duree",
                table: "Taches",
                newName: "duree");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Taches",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Taches",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "IdEmploye",
                table: "Conges",
                newName: "idEmploye");

            migrationBuilder.RenameColumn(
                name: "Duree",
                table: "Conges",
                newName: "duree");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Conges",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Conges",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Assignations",
                newName: "message");

            migrationBuilder.RenameColumn(
                name: "IdTache",
                table: "Assignations",
                newName: "idTache");

            migrationBuilder.RenameColumn(
                name: "IdEmploye",
                table: "Assignations",
                newName: "idEmploye");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Assignations",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "Pole",
                table: "Employes",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
