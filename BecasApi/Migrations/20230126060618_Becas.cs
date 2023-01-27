using Microsoft.EntityFrameworkCore.Migrations;

namespace BecasApi.Migrations
{
    public partial class Becas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Alumnos",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateTable(
                name: "Becas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Becas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BecasAlumnos",
                columns: table => new
                {
                    BecaId = table.Column<int>(type: "int", nullable: false),
                    AlumnoId = table.Column<int>(type: "int", nullable: false),
                    BecasId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BecasAlumnos", x => new { x.BecaId, x.AlumnoId });
                    table.ForeignKey(
                        name: "FK_BecasAlumnos_Alumnos_AlumnoId",
                        column: x => x.AlumnoId,
                        principalTable: "Alumnos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BecasAlumnos_Becas_BecasId",
                        column: x => x.BecasId,
                        principalTable: "Becas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BecasAlumnos_AlumnoId",
                table: "BecasAlumnos",
                column: "AlumnoId");

            migrationBuilder.CreateIndex(
                name: "IX_BecasAlumnos_BecasId",
                table: "BecasAlumnos",
                column: "BecasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BecasAlumnos");

            migrationBuilder.DropTable(
                name: "Becas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Alumnos",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);
        }
    }
}
