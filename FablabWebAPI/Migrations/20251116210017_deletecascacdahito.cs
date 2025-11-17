using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FablabWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class deletecascacdahito : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HitoProyecto_Proyectos_ProyectosId",
                table: "HitoProyecto");

            migrationBuilder.AddForeignKey(
                name: "FK_HitoProyecto_Proyectos_ProyectosId",
                table: "HitoProyecto",
                column: "ProyectosId",
                principalTable: "Proyectos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HitoProyecto_Proyectos_ProyectosId",
                table: "HitoProyecto");

            migrationBuilder.AddForeignKey(
                name: "FK_HitoProyecto_Proyectos_ProyectosId",
                table: "HitoProyecto",
                column: "ProyectosId",
                principalTable: "Proyectos",
                principalColumn: "Id");
        }
    }
}
