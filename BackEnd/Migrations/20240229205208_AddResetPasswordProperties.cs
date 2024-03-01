using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddResetPasswordProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReinitialiserMDPJetonExpiration",
                table: "Employes",
                newName: "ResetTokenExpires");

            migrationBuilder.RenameColumn(
                name: "ReinitialiserMDPJeton",
                table: "Employes",
                newName: "ResetToken");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ResetTokenExpires",
                table: "Employes",
                newName: "ReinitialiserMDPJetonExpiration");

            migrationBuilder.RenameColumn(
                name: "ResetToken",
                table: "Employes",
                newName: "ReinitialiserMDPJeton");
        }
    }
}
