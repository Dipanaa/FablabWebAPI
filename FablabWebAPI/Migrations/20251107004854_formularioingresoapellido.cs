using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FablabWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class formularioingresoapellido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apellido",
                table: "FormulariosIngreso",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Apellido",
                table: "FormulariosIngreso");
        }
    }
}
