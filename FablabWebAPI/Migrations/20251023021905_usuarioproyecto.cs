using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FablabWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class usuarioproyecto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_AspNetUsers_UsuarioId",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Proyectos");

            migrationBuilder.CreateTable(
                name: "UsuarioProyecto",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ProyectosId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioProyecto", x => new { x.UsuarioId, x.ProyectosId });
                    table.ForeignKey(
                        name: "FK_UsuarioProyecto_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioProyecto_Proyectos_ProyectosId",
                        column: x => x.ProyectosId,
                        principalTable: "Proyectos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioProyecto_ProyectosId",
                table: "UsuarioProyecto",
                column: "ProyectosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuarioProyecto");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Proyectos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_AspNetUsers_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
