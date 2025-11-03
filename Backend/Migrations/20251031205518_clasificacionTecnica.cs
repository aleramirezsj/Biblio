using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class clasificacionTecnica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CDU",
                table: "Libros",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Libristica",
                table: "Libros",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "PalabrasClave",
                table: "Libros",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });

            migrationBuilder.UpdateData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CDU", "Libristica", "PalabrasClave" },
                values: new object[] { "", "", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CDU",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "Libristica",
                table: "Libros");

            migrationBuilder.DropColumn(
                name: "PalabrasClave",
                table: "Libros");
        }
    }
}
