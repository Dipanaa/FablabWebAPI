using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FablabWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class migraciondelaboratorio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LaboratorioId",
                table: "Noticias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Laboratorio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreLaboratorio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CantidadIntegrantes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laboratorio", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Noticias_LaboratorioId",
                table: "Noticias",
                column: "LaboratorioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Noticias_Laboratorio_LaboratorioId",
                table: "Noticias",
                column: "LaboratorioId",
                principalTable: "Laboratorio",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Noticias_Laboratorio_LaboratorioId",
                table: "Noticias");

            migrationBuilder.DropTable(
                name: "Laboratorio");

            migrationBuilder.DropIndex(
                name: "IX_Noticias_LaboratorioId",
                table: "Noticias");

            migrationBuilder.DropColumn(
                name: "LaboratorioId",
                table: "Noticias");
        }
    }
}
