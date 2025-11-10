using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class idsCarreras : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Técnico Superior en Desarrollo de Software");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Técnico Superior en Soporte de Infraestructura en Tecnologías de la Información");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Técnico Superior en Gestión de las Organizaciones");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Nombre",
                value: "Técnico Superior en Enfermería");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nombre",
                value: "Profesorado de Educ. Secundaria en Cs de la Administración");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 6,
                column: "Nombre",
                value: "Profesorado de Educación Inicial");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 7,
                column: "Nombre",
                value: "Profesorado de Educ. Secundaria en Economía");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 8,
                column: "Nombre",
                value: "Profesorado de Educación Tecnológica");

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[] { 22, false, "Tecnicatura Superior en Gestión de Energías Renovables" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 1,
                column: "Nombre",
                value: "Ingeniería Informática");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 2,
                column: "Nombre",
                value: "Derecho");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 3,
                column: "Nombre",
                value: "Medicina");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 4,
                column: "Nombre",
                value: "Psicología");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 5,
                column: "Nombre",
                value: "Arquitectura");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 6,
                column: "Nombre",
                value: "Administración");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 7,
                column: "Nombre",
                value: "Contabilidad");

            migrationBuilder.UpdateData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 8,
                column: "Nombre",
                value: "Educación");

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 9, false, "Biología" },
                    { 10, false, "Física" }
                });
        }
    }
}
