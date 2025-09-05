using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class datosSemillaRestantes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Ingeniería Informática" },
                    { 2, false, "Derecho" },
                    { 3, false, "Medicina" },
                    { 4, false, "Psicología" },
                    { 5, false, "Arquitectura" },
                    { 6, false, "Administración" },
                    { 7, false, "Contabilidad" },
                    { 8, false, "Educación" },
                    { 9, false, "Biología" },
                    { 10, false, "Física" }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AnioPublicacion", "Descripcion", "EditorialId", "IsDeleted", "Paginas", "Portada", "Sinopsis", "Titulo" },
                values: new object[,]
                {
                    { 1, 1967, "Novela emblemática", 1, false, 471, "portada1.jpg", "La historia de la familia Buendía.", "Cien años de soledad" },
                    { 2, 1982, "Realismo mágico chileno", 2, false, 368, "portada2.jpg", "Saga familiar de los Trueba.", "La casa de los espíritus" },
                    { 3, 1969, "Corrupción y dictadura", 3, false, 601, "portada3.jpg", "La vida bajo la dictadura de Odría.", "Conversación en La Catedral" },
                    { 4, 1923, "Poesía argentina", 4, false, 120, "portada4.jpg", "Primer libro de Borges.", "Fervor de Buenos Aires" },
                    { 5, 1989, "Novela de realismo mágico", 5, false, 256, "portada5.jpg", "La historia de Tita y su cocina.", "Como agua para chocolate" },
                    { 6, 1962, "Novela mexicana", 6, false, 336, "portada6.jpg", "La vida de Artemio Cruz.", "La muerte de Artemio Cruz" },
                    { 7, 1963, "Novela experimental", 7, false, 736, "portada7.jpg", "La vida de Horacio Oliveira.", "Rayuela" },
                    { 8, 1605, "Clásico español", 8, false, 863, "portada8.jpg", "Las aventuras de Don Quijote.", "Don Quijote de la Mancha" },
                    { 9, 1924, "Poesía chilena", 9, false, 64, "portada9.jpg", "Poemas de amor de Neruda.", "Veinte poemas de amor" },
                    { 10, 1950, "Ensayo mexicano", 10, false, 228, "portada10.jpg", "Reflexión sobre la identidad mexicana.", "El laberinto de la soledad" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Dni", "Domicilio", "Email", "FechaRegistracion", "IsDeleted", "Nombre", "Observacion", "Password", "Telefono", "TipoRol" },
                values: new object[,]
                {
                    { 1, "10000001", "Calle 1", "juan1@mail.com", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Juan Pérez", "", "pass1", "1111111", 0 },
                    { 2, "10000002", "Calle 2", "ana2@mail.com", new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Ana Gómez", "", "pass2", "2222222", 1 },
                    { 3, "10000003", "Calle 3", "luis3@mail.com", new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Luis Torres", "", "pass3", "3333333", 2 },
                    { 4, "10000004", "Calle 4", "maria4@mail.com", new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "María López", "", "pass4", "4444444", 0 },
                    { 5, "10000005", "Calle 5", "pedro5@mail.com", new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Pedro Ruiz", "", "pass5", "5555555", 1 },
                    { 6, "10000006", "Calle 6", "lucia6@mail.com", new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Lucía Fernández", "", "pass6", "6666666", 0 },
                    { 7, "10000007", "Calle 7", "carlos7@mail.com", new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Carlos Díaz", "", "pass7", "7777777", 2 },
                    { 8, "10000008", "Calle 8", "sofia8@mail.com", new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Sofía Ramírez", "", "pass8", "8888888", 0 },
                    { 9, "10000009", "Calle 9", "miguel9@mail.com", new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Miguel Castro", "", "pass9", "9999999", 1 },
                    { 10, "10000010", "Calle 10", "elena10@mail.com", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, "Elena Vargas", "", "pass10", "10101010", 0 }
                });

            migrationBuilder.InsertData(
                table: "Ejemplares",
                columns: new[] { "Id", "Disponible", "Estado", "IsDeleted", "LibroId" },
                values: new object[,]
                {
                    { 1, true, 0, false, 1 },
                    { 2, true, 1, false, 2 },
                    { 3, true, 2, false, 3 },
                    { 4, true, 3, false, 4 },
                    { 5, true, 4, false, 5 },
                    { 6, true, 0, false, 6 },
                    { 7, true, 1, false, 7 },
                    { 8, true, 2, false, 8 },
                    { 9, true, 3, false, 9 },
                    { 10, true, 4, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "LibroAutores",
                columns: new[] { "Id", "AutorId", "IsDeleted", "LibroId" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 5, false, 5 },
                    { 6, 6, false, 6 },
                    { 7, 7, false, 7 },
                    { 8, 8, false, 8 },
                    { 9, 9, false, 9 },
                    { 10, 10, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "LibroGeneros",
                columns: new[] { "Id", "GeneroId", "IsDeleted", "LibroId" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 5, false, 5 },
                    { 6, 6, false, 6 },
                    { 7, 7, false, 7 },
                    { 8, 8, false, 8 },
                    { 9, 9, false, 9 },
                    { 10, 10, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "UsuarioCarreras",
                columns: new[] { "Id", "CarreraId", "IsDeleted", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 5, false, 5 },
                    { 6, 6, false, 6 },
                    { 7, 7, false, 7 },
                    { 8, 8, false, 8 },
                    { 9, 9, false, 9 },
                    { 10, 10, false, 10 }
                });

            migrationBuilder.InsertData(
                table: "Prestamos",
                columns: new[] { "Id", "EjemplarId", "FechaDevolucion", "FechaPrestamo", "IsDeleted", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1 },
                    { 2, 2, new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2 },
                    { 3, 3, new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3 },
                    { 4, 4, new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4 },
                    { 5, 5, new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5 },
                    { 6, 6, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 6 },
                    { 7, 7, new DateTime(2023, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 7 },
                    { 8, 8, new DateTime(2023, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 8 },
                    { 9, 9, new DateTime(2023, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 9 },
                    { 10, 10, new DateTime(2023, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LibroAutores",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "LibroGeneros",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Prestamos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "UsuarioCarreras",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Carreras",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ejemplares",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Libros",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
