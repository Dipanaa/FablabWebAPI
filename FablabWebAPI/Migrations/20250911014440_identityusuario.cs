using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FablabWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class identityusuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_Usuario_UsuarioId",
                table: "Proyectos");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Proyectos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Carrera",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Contraseña",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CorreoInstitucional",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "LaboratorioId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nombre",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RolId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Rut",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UsuarioId1",
                table: "Proyectos",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LaboratorioId",
                table: "AspNetUsers",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RolId",
                table: "AspNetUsers",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Laboratorio_LaboratorioId",
                table: "AspNetUsers",
                column: "LaboratorioId",
                principalTable: "Laboratorio",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rol_RolId",
                table: "AspNetUsers",
                column: "RolId",
                principalTable: "Rol",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_AspNetUsers_UsuarioId1",
                table: "Proyectos",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Laboratorio_LaboratorioId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rol_RolId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Proyectos_AspNetUsers_UsuarioId1",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_Proyectos_UsuarioId1",
                table: "Proyectos");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LaboratorioId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Proyectos");

            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Carrera",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Contraseña",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CorreoInstitucional",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LaboratorioId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Nombre",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RolId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Rut",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaboratorioId = table.Column<int>(type: "int", nullable: true),
                    RolId = table.Column<int>(type: "int", nullable: true),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Carrera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contraseña = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorreoInstitucional = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rut = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuario_Laboratorio_LaboratorioId",
                        column: x => x.LaboratorioId,
                        principalTable: "Laboratorio",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Usuario_Rol_RolId",
                        column: x => x.RolId,
                        principalTable: "Rol",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_LaboratorioId",
                table: "Usuario",
                column: "LaboratorioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_RolId",
                table: "Usuario",
                column: "RolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proyectos_Usuario_UsuarioId",
                table: "Proyectos",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }
    }
}
