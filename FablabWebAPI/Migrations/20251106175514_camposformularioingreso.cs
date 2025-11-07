using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FablabWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class camposformularioingreso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CorreoInstitucional",
                table: "FormulariosIngreso",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "Contraseña",
                table: "FormulariosIngreso",
                newName: "Contrasena");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Email",
                table: "FormulariosIngreso",
                newName: "CorreoInstitucional");

            migrationBuilder.RenameColumn(
                name: "Contrasena",
                table: "FormulariosIngreso",
                newName: "Contraseña");
        }
    }
}
