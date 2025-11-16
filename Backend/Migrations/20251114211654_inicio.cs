using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Pgvector;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class inicio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:vector", ",,");

            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Editoriales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Editoriales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    TipoRol = table.Column<int>(type: "integer", nullable: false),
                    FechaRegistracion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Dni = table.Column<string>(type: "text", nullable: false),
                    Domicilio = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Observacion = table.Column<string>(type: "text", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false),
                    EditorialId = table.Column<int>(type: "integer", nullable: false),
                    Paginas = table.Column<int>(type: "integer", nullable: false),
                    AnioPublicacion = table.Column<int>(type: "integer", nullable: false),
                    Portada = table.Column<string>(type: "text", nullable: false),
                    Sinopsis = table.Column<string>(type: "text", nullable: false),
                    CDU = table.Column<string>(type: "text", nullable: false),
                    Libristica = table.Column<string>(type: "text", nullable: false),
                    PalabrasClave = table.Column<string>(type: "text", nullable: false),
                    SinopsisEmbedding = table.Column<Vector>(type: "vector(1536)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Libros_Editoriales_EditorialId",
                        column: x => x.EditorialId,
                        principalTable: "Editoriales",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioCarreras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    CarreraId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioCarreras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuarioCarreras_Carreras_CarreraId",
                        column: x => x.CarreraId,
                        principalTable: "Carreras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioCarreras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ejemplares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LibroId = table.Column<int>(type: "integer", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false),
                    Estado = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ejemplares", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ejemplares_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibroAutores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LibroId = table.Column<int>(type: "integer", nullable: false),
                    AutorId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroAutores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibroAutores_Autores_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibroAutores_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibroGeneros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LibroId = table.Column<int>(type: "integer", nullable: false),
                    GeneroId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroGeneros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibroGeneros_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibroGeneros_Libros_LibroId",
                        column: x => x.LibroId,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    EjemplarId = table.Column<int>(type: "integer", nullable: false),
                    FechaPrestamo = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    FechaDevolucion = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Ejemplares_EjemplarId",
                        column: x => x.EjemplarId,
                        principalTable: "Ejemplares",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Prestamos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autores",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Gabriel García Márquez" },
                    { 2, false, "Isabel Allende" },
                    { 3, false, "Mario Vargas Llosa" },
                    { 4, false, "Jorge Luis Borges" },
                    { 5, false, "Laura Esquivel" },
                    { 6, false, "Carlos Fuentes" },
                    { 7, false, "Julio Cortázar" },
                    { 8, false, "Miguel de Cervantes" },
                    { 9, false, "Pablo Neruda" },
                    { 10, false, "Octavio Paz" }
                });

            migrationBuilder.InsertData(
                table: "Carreras",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Técnico Superior en Desarrollo de Software" },
                    { 2, false, "Técnico Superior en Soporte de Infraestructura en Tecnologías de la Información" },
                    { 3, false, "Técnico Superior en Gestión de las Organizaciones" },
                    { 4, false, "Técnico Superior en Enfermería" },
                    { 5, false, "Profesorado de Educ. Secundaria en Cs de la Administración" },
                    { 6, false, "Profesorado de Educación Inicial" },
                    { 7, false, "Profesorado de Educ. Secundaria en Economía" },
                    { 8, false, "Profesorado de Educación Tecnológica" },
                    { 22, false, "Tecnicatura Superior en Gestión de Energías Renovables" }
                });

            migrationBuilder.InsertData(
                table: "Editoriales",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Penguin Random House" },
                    { 2, false, "HarperCollins" },
                    { 3, false, "Simon & Schuster" },
                    { 4, false, "Hachette Book Group" },
                    { 5, false, "Macmillan Publishers" },
                    { 6, false, "Scholastic" },
                    { 7, false, "Bloomsbury Publishing" },
                    { 8, false, "Oxford University Press" },
                    { 9, false, "Cambridge University Press" },
                    { 10, false, "Wiley" }
                });

            migrationBuilder.InsertData(
                table: "Generos",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "Ficción" },
                    { 2, false, "No Ficción" },
                    { 3, false, "Misterio" },
                    { 4, false, "Romance" },
                    { 5, false, "Ciencia Ficción" },
                    { 6, false, "Fantasia" },
                    { 7, false, "Historia" },
                    { 8, false, "Biografía" },
                    { 9, false, "Poesía" },
                    { 10, false, "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Dni", "Domicilio", "Email", "FechaRegistracion", "IsDeleted", "Nombre", "Observacion", "Password", "Telefono", "TipoRol" },
                values: new object[,]
                {
                    { 1, "10000001", "Calle 1", "juan1@mail.com", new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Juan Pérez", "", "pass1", "1111111", 0 },
                    { 2, "10000002", "Calle 2", "ana2@mail.com", new DateTimeOffset(new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Ana Gómez", "", "pass2", "2222222", 1 },
                    { 3, "10000003", "Calle 3", "luis3@mail.com", new DateTimeOffset(new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Luis Torres", "", "pass3", "3333333", 2 },
                    { 4, "10000004", "Calle 4", "maria4@mail.com", new DateTimeOffset(new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "María López", "", "pass4", "4444444", 0 },
                    { 5, "10000005", "Calle 5", "pedro5@mail.com", new DateTimeOffset(new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Pedro Ruiz", "", "pass5", "5555555", 1 },
                    { 6, "10000006", "Calle 6", "lucia6@mail.com", new DateTimeOffset(new DateTime(2023, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Lucía Fernández", "", "pass6", "6666666", 0 },
                    { 7, "10000007", "Calle 7", "carlos7@mail.com", new DateTimeOffset(new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Carlos Díaz", "", "pass7", "7777777", 2 },
                    { 8, "10000008", "Calle 8", "sofia8@mail.com", new DateTimeOffset(new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Sofía Ramírez", "", "pass8", "8888888", 0 },
                    { 9, "10000009", "Calle 9", "miguel9@mail.com", new DateTimeOffset(new DateTime(2023, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Miguel Castro", "", "pass9", "9999999", 1 },
                    { 10, "10000010", "Calle 10", "elena10@mail.com", new DateTimeOffset(new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Elena Vargas", "", "pass10", "10101010", 0 }
                });

            migrationBuilder.InsertData(
                table: "Libros",
                columns: new[] { "Id", "AnioPublicacion", "CDU", "Descripcion", "EditorialId", "IsDeleted", "Libristica", "Paginas", "PalabrasClave", "Portada", "Sinopsis", "SinopsisEmbedding", "Titulo" },
                values: new object[,]
                {
                    { 1, 1967, "", "Novela emblemática", 1, false, "", 471, "", "portada1.jpg", "La historia de la familia Buendía.", null, "Cien años de soledad" },
                    { 2, 1982, "", "Realismo mágico chileno", 2, false, "", 368, "", "portada2.jpg", "Saga familiar de los Trueba.", null, "La casa de los espíritus" },
                    { 3, 1969, "", "Corrupción y dictadura", 3, false, "", 601, "", "portada3.jpg", "La vida bajo la dictadura de Odría.", null, "Conversación en La Catedral" },
                    { 4, 1923, "", "Poesía argentina", 4, false, "", 120, "", "portada4.jpg", "Primer libro de Borges.", null, "Fervor de Buenos Aires" },
                    { 5, 1989, "", "Novela de realismo mágico", 5, false, "", 256, "", "portada5.jpg", "La historia de Tita y su cocina.", null, "Como agua para chocolate" },
                    { 6, 1962, "", "Novela mexicana", 6, false, "", 336, "", "portada6.jpg", "La vida de Artemio Cruz.", null, "La muerte de Artemio Cruz" },
                    { 7, 1963, "", "Novela experimental", 7, false, "", 736, "", "portada7.jpg", "La vida de Horacio Oliveira.", null, "Rayuela" },
                    { 8, 1605, "", "Clásico español", 8, false, "", 863, "", "portada8.jpg", "Las aventuras de Don Quijote.", null, "Don Quijote de la Mancha" },
                    { 9, 1924, "", "Poesía chilena", 9, false, "", 64, "", "portada9.jpg", "Poemas de amor de Neruda.", null, "Veinte poemas de amor" },
                    { 10, 1950, "", "Ensayo mexicano", 10, false, "", 228, "", "portada10.jpg", "Reflexión sobre la identidad mexicana.", null, "El laberinto de la soledad" }
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
                    { 9, 22, false, 9 }
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
                table: "Prestamos",
                columns: new[] { "Id", "EjemplarId", "FechaDevolucion", "FechaPrestamo", "IsDeleted", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 1 },
                    { 2, 2, new DateTimeOffset(new DateTime(2023, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 2 },
                    { 3, 3, new DateTimeOffset(new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 3 },
                    { 4, 4, new DateTimeOffset(new DateTime(2023, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 4 },
                    { 5, 5, new DateTimeOffset(new DateTime(2023, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 5 },
                    { 6, 6, new DateTimeOffset(new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 6 },
                    { 7, 7, new DateTimeOffset(new DateTime(2023, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 7 },
                    { 8, 8, new DateTimeOffset(new DateTime(2023, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 8 },
                    { 9, 9, new DateTimeOffset(new DateTime(2023, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 9 },
                    { 10, 10, new DateTimeOffset(new DateTime(2023, 2, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2023, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ejemplares_LibroId",
                table: "Ejemplares",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutores_AutorId",
                table: "LibroAutores",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutores_LibroId",
                table: "LibroAutores",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_LibroGeneros_GeneroId",
                table: "LibroGeneros",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_LibroGeneros_LibroId",
                table: "LibroGeneros",
                column: "LibroId");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_EditorialId",
                table: "Libros",
                column: "EditorialId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_EjemplarId",
                table: "Prestamos",
                column: "EjemplarId");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_UsuarioId",
                table: "Prestamos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCarreras_CarreraId",
                table: "UsuarioCarreras",
                column: "CarreraId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioCarreras_UsuarioId",
                table: "UsuarioCarreras",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LibroAutores");

            migrationBuilder.DropTable(
                name: "LibroGeneros");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "UsuarioCarreras");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Ejemplares");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Editoriales");
        }
    }
}
