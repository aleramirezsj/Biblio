using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Backend.DataContext
{
    public class BiblioContext: DbContext
    {
        public virtual DbSet<Libro> Libros { get; set; } 
        public virtual DbSet<Ejemplar> Ejemplares { get; set; }
        public virtual DbSet<Autor> Autores { get; set; }
        public virtual DbSet<Genero> Generos { get; set; }
        public virtual DbSet<LibroAutor> LibroAutores { get; set; }
        public virtual DbSet<LibroGenero> LibroGeneros { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Prestamo> Prestamos { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<UsuarioCarrera> UsuarioCarreras { get; set; }
        public virtual DbSet<Editorial> Editoriales { get; set; }

        public BiblioContext()
        {
            
        }
        public BiblioContext(DbContextOptions<BiblioContext> options) : base(options)
        {
        }
        // onConfiguring method to set up the database connection
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
                var connectionString = configuration.GetConnectionString("mysqlRemote");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region datos semilla de 10 autores
            modelBuilder.Entity<Autor>().HasData(
                new Autor { Id = 1, Nombre = "Gabriel García Márquez" },
                new Autor { Id = 2, Nombre = "Isabel Allende" },
                new Autor { Id = 3, Nombre = "Mario Vargas Llosa" },
                new Autor { Id = 4, Nombre = "Jorge Luis Borges" },
                new Autor { Id = 5, Nombre = "Laura Esquivel" },
                new Autor { Id = 6, Nombre = "Carlos Fuentes" },
                new Autor { Id = 7, Nombre = "Julio Cortázar" },
                new Autor { Id = 8, Nombre = "Miguel de Cervantes" },
                new Autor { Id = 9, Nombre = "Pablo Neruda" },
                new Autor { Id = 10, Nombre = "Octavio Paz" }
            );
            #endregion
            # region datos semilla de 10 generos
            modelBuilder.Entity<Genero>().HasData(
                new Genero { Id = 1, Nombre = "Ficción" },
                new Genero { Id = 2, Nombre = "No Ficción" },
                new Genero { Id = 3, Nombre = "Misterio" },
                new Genero { Id = 4, Nombre = "Romance" },
                new Genero { Id = 5, Nombre = "Ciencia Ficción" },
                new Genero { Id = 6, Nombre = "Fantasia" },
                new Genero { Id = 7, Nombre = "Historia" },
                new Genero { Id = 8, Nombre = "Biografía" },
                new Genero { Id = 9, Nombre = "Poesía" },
                new Genero { Id = 10, Nombre = "Drama" }
            );
            #endregion
            #region datos semilla de 10 editoriales
            modelBuilder.Entity<Editorial>().HasData(
                new Editorial { Id = 1, Nombre = "Penguin Random House" },
                new Editorial { Id = 2, Nombre = "HarperCollins" },
                new Editorial { Id = 3, Nombre = "Simon & Schuster" },
                new Editorial { Id = 4, Nombre = "Hachette Book Group" },
                new Editorial { Id = 5, Nombre = "Macmillan Publishers" },
                new Editorial { Id = 6, Nombre = "Scholastic" },
                new Editorial { Id = 7, Nombre = "Bloomsbury Publishing" },
                new Editorial { Id = 8, Nombre = "Oxford University Press" },
                new Editorial { Id = 9, Nombre = "Cambridge University Press" },
                new Editorial { Id = 10, Nombre = "Wiley" }
            );
            #endregion
            #region datos semilla de 10 libros
            modelBuilder.Entity<Libro>().HasData(
                new Libro { Id = 1, Titulo = "Cien años de soledad", Descripcion = "Novela emblemática", EditorialId = 1, Paginas = 471, AnioPublicacion = 1967, Portada = "portada1.jpg", Sinopsis = "La historia de la familia Buendía.", IsDeleted = false },
                new Libro { Id = 2, Titulo = "La casa de los espíritus", Descripcion = "Realismo mágico chileno", EditorialId = 2, Paginas = 368, AnioPublicacion = 1982, Portada = "portada2.jpg", Sinopsis = "Saga familiar de los Trueba.", IsDeleted = false },
                new Libro { Id = 3, Titulo = "Conversación en La Catedral", Descripcion = "Corrupción y dictadura", EditorialId = 3, Paginas = 601, AnioPublicacion = 1969, Portada = "portada3.jpg", Sinopsis = "La vida bajo la dictadura de Odría.", IsDeleted = false },
                new Libro { Id = 4, Titulo = "Fervor de Buenos Aires", Descripcion = "Poesía argentina", EditorialId = 4, Paginas = 120, AnioPublicacion = 1923, Portada = "portada4.jpg", Sinopsis = "Primer libro de Borges.", IsDeleted = false },
                new Libro { Id = 5, Titulo = "Como agua para chocolate", Descripcion = "Novela de realismo mágico", EditorialId = 5, Paginas = 256, AnioPublicacion = 1989, Portada = "portada5.jpg", Sinopsis = "La historia de Tita y su cocina.", IsDeleted = false },
                new Libro { Id = 6, Titulo = "La muerte de Artemio Cruz", Descripcion = "Novela mexicana", EditorialId = 6, Paginas = 336, AnioPublicacion = 1962, Portada = "portada6.jpg", Sinopsis = "La vida de Artemio Cruz.", IsDeleted = false },
                new Libro { Id = 7, Titulo = "Rayuela", Descripcion = "Novela experimental", EditorialId = 7, Paginas = 736, AnioPublicacion = 1963, Portada = "portada7.jpg", Sinopsis = "La vida de Horacio Oliveira.", IsDeleted = false },
                new Libro { Id = 8, Titulo = "Don Quijote de la Mancha", Descripcion = "Clásico español", EditorialId = 8, Paginas = 863, AnioPublicacion = 1605, Portada = "portada8.jpg", Sinopsis = "Las aventuras de Don Quijote.", IsDeleted = false },
                new Libro { Id = 9, Titulo = "Veinte poemas de amor", Descripcion = "Poesía chilena", EditorialId = 9, Paginas = 64, AnioPublicacion = 1924, Portada = "portada9.jpg", Sinopsis = "Poemas de amor de Neruda.", IsDeleted = false },
                new Libro { Id = 10, Titulo = "El laberinto de la soledad", Descripcion = "Ensayo mexicano", EditorialId = 10, Paginas = 228, AnioPublicacion = 1950, Portada = "portada10.jpg", Sinopsis = "Reflexión sobre la identidad mexicana.", IsDeleted = false }
);
#endregion
#region datos semilla de 10 usuarios
modelBuilder.Entity<Usuario>().HasData(
    new Usuario { Id = 1, Nombre = "Juan Pérez", Email = "juan1@mail.com", Password = "pass1", TipoRol = Service.Enums.TipoRolEnum.Alumno, FechaRegistracion = new DateTime(2023,1,1), Dni = "10000001", Domicilio = "Calle 1", Telefono = "1111111", Observacion = "", IsDeleted = false },
    new Usuario { Id = 2, Nombre = "Ana Gómez", Email = "ana2@mail.com", Password = "pass2", TipoRol = Service.Enums.TipoRolEnum.Docente, FechaRegistracion = new DateTime(2023,1,2), Dni = "10000002", Domicilio = "Calle 2", Telefono = "2222222", Observacion = "", IsDeleted = false },
    new Usuario { Id = 3, Nombre = "Luis Torres", Email = "luis3@mail.com", Password = "pass3", TipoRol = Service.Enums.TipoRolEnum.Bibliotecario, FechaRegistracion = new DateTime(2023,1,3), Dni = "10000003", Domicilio = "Calle 3", Telefono = "3333333", Observacion = "", IsDeleted = false },
    new Usuario { Id = 4, Nombre = "María López", Email = "maria4@mail.com", Password = "pass4", TipoRol = Service.Enums.TipoRolEnum.Alumno, FechaRegistracion = new DateTime(2023,1,4), Dni = "10000004", Domicilio = "Calle 4", Telefono = "4444444", Observacion = "", IsDeleted = false },
    new Usuario { Id = 5, Nombre = "Pedro Ruiz", Email = "pedro5@mail.com", Password = "pass5", TipoRol = Service.Enums.TipoRolEnum.Docente, FechaRegistracion = new DateTime(2023,1,5), Dni = "10000005", Domicilio = "Calle 5", Telefono = "5555555", Observacion = "", IsDeleted = false },
    new Usuario { Id = 6, Nombre = "Lucía Fernández", Email = "lucia6@mail.com", Password = "pass6", TipoRol = Service.Enums.TipoRolEnum.Alumno, FechaRegistracion = new DateTime(2023,1,6), Dni = "10000006", Domicilio = "Calle 6", Telefono = "6666666", Observacion = "", IsDeleted = false },
    new Usuario { Id = 7, Nombre = "Carlos Díaz", Email = "carlos7@mail.com", Password = "pass7", TipoRol = Service.Enums.TipoRolEnum.Bibliotecario, FechaRegistracion = new DateTime(2023,1,7), Dni = "10000007", Domicilio = "Calle 7", Telefono = "7777777", Observacion = "", IsDeleted = false },
    new Usuario { Id = 8, Nombre = "Sofía Ramírez", Email = "sofia8@mail.com", Password = "pass8", TipoRol = Service.Enums.TipoRolEnum.Alumno, FechaRegistracion = new DateTime(2023,1,8), Dni = "10000008", Domicilio = "Calle 8", Telefono = "8888888", Observacion = "", IsDeleted = false },
    new Usuario { Id = 9, Nombre = "Miguel Castro", Email = "miguel9@mail.com", Password = "pass9", TipoRol = Service.Enums.TipoRolEnum.Docente, FechaRegistracion = new DateTime(2023,1,9), Dni = "10000009", Domicilio = "Calle 9", Telefono = "9999999", Observacion = "", IsDeleted = false },
    new Usuario { Id = 10, Nombre = "Elena Vargas", Email = "elena10@mail.com", Password = "pass10", TipoRol = Service.Enums.TipoRolEnum.Alumno, FechaRegistracion = new DateTime(2023,1,10), Dni = "10000010", Domicilio = "Calle 10", Telefono = "10101010", Observacion = "", IsDeleted = false }
);
            #endregion
            #region datos semillas de 9 carreras
            modelBuilder.Entity<Carrera>().HasData(
                new Carrera { Id = 6, Nombre = "Profesorado de Educación Inicial" },
                new Carrera { Id = 5, Nombre = "Profesorado de Educ. Secundaria en Cs de la Administración" },
                new Carrera { Id = 7, Nombre = "Profesorado de Educ. Secundaria en Economía" },
                new Carrera { Id = 8, Nombre = "Profesorado de Educación Tecnológica" },
                new Carrera { Id = 1, Nombre = "Técnico Superior en Desarrollo de Software" },
                new Carrera { Id = 4, Nombre = "Técnico Superior en Enfermería" },
                new Carrera { Id = 22, Nombre = "Tecnicatura Superior en Gestión de Energías Renovables" },
                new Carrera { Id = 3, Nombre = "Técnico Superior en Gestión de las Organizaciones" },
                new Carrera { Id = 2, Nombre = "Técnico Superior en Soporte de Infraestructura en Tecnologías de la Información" }
            );
            #endregion
            #region datos semilla de 10 ejemplares
            modelBuilder.Entity<Ejemplar>().HasData(
    new Ejemplar { Id = 1, LibroId = 1, Disponible = true, Estado = Service.Enums.EstadoEnum.Excelente, IsDeleted = false },
    new Ejemplar { Id = 2, LibroId = 2, Disponible = true, Estado = Service.Enums.EstadoEnum.MuyBueno, IsDeleted = false },
    new Ejemplar { Id = 3, LibroId = 3, Disponible = true, Estado = Service.Enums.EstadoEnum.Bueno, IsDeleted = false },
    new Ejemplar { Id = 4, LibroId = 4, Disponible = true, Estado = Service.Enums.EstadoEnum.Regular, IsDeleted = false },
    new Ejemplar { Id = 5, LibroId = 5, Disponible = true, Estado = Service.Enums.EstadoEnum.Malo, IsDeleted = false },
    new Ejemplar { Id = 6, LibroId = 6, Disponible = true, Estado = Service.Enums.EstadoEnum.Excelente, IsDeleted = false },
    new Ejemplar { Id = 7, LibroId = 7, Disponible = true, Estado = Service.Enums.EstadoEnum.MuyBueno, IsDeleted = false },
    new Ejemplar { Id = 8, LibroId = 8, Disponible = true, Estado = Service.Enums.EstadoEnum.Bueno, IsDeleted = false },
    new Ejemplar { Id = 9, LibroId = 9, Disponible = true, Estado = Service.Enums.EstadoEnum.Regular, IsDeleted = false },
    new Ejemplar { Id = 10, LibroId = 10, Disponible = true, Estado = Service.Enums.EstadoEnum.Malo, IsDeleted = false }
);
#endregion
#region datos semilla de 10 prestamos
modelBuilder.Entity<Prestamo>().HasData(
    new Prestamo { Id = 1, UsuarioId = 1, EjemplarId = 1, FechaPrestamo = new DateTime(2023,2,1), FechaDevolucion = new DateTime(2023,2,10), IsDeleted = false },
    new Prestamo { Id = 2, UsuarioId = 2, EjemplarId = 2, FechaPrestamo = new DateTime(2023,2,2), FechaDevolucion = new DateTime(2023,2,11), IsDeleted = false },
    new Prestamo { Id = 3, UsuarioId = 3, EjemplarId = 3, FechaPrestamo = new DateTime(2023,2,3), FechaDevolucion = new DateTime(2023,2,12), IsDeleted = false },
    new Prestamo { Id = 4, UsuarioId = 4, EjemplarId = 4, FechaPrestamo = new DateTime(2023,2,4), FechaDevolucion = new DateTime(2023,2,13), IsDeleted = false },
    new Prestamo { Id = 5, UsuarioId = 5, EjemplarId = 5, FechaPrestamo = new DateTime(2023,2,5), FechaDevolucion = new DateTime(2023,2,14), IsDeleted = false },
    new Prestamo { Id = 6, UsuarioId = 6, EjemplarId = 6, FechaPrestamo = new DateTime(2023,2,6), FechaDevolucion = new DateTime(2023,2,15), IsDeleted = false },
    new Prestamo { Id = 7, UsuarioId = 7, EjemplarId = 7, FechaPrestamo = new DateTime(2023,2,7), FechaDevolucion = new DateTime(2023,2,16), IsDeleted = false },
    new Prestamo { Id = 8, UsuarioId = 8, EjemplarId = 8, FechaPrestamo = new DateTime(2023,2,8), FechaDevolucion = new DateTime(2023,2,17), IsDeleted = false },
    new Prestamo { Id = 9, UsuarioId = 9, EjemplarId = 9, FechaPrestamo = new DateTime(2023,2,9), FechaDevolucion = new DateTime(2023,2,18), IsDeleted = false },
    new Prestamo { Id = 10, UsuarioId = 10, EjemplarId = 10, FechaPrestamo = new DateTime(2023,2,10), FechaDevolucion = new DateTime(2023,2,19), IsDeleted = false }
);
#endregion
#region datos semilla de 10 libroautor
modelBuilder.Entity<LibroAutor>().HasData(
    new LibroAutor { Id = 1, LibroId = 1, AutorId = 1, IsDeleted = false },
    new LibroAutor { Id = 2, LibroId = 2, AutorId = 2, IsDeleted = false },
    new LibroAutor { Id = 3, LibroId = 3, AutorId = 3, IsDeleted = false },
    new LibroAutor { Id = 4, LibroId = 4, AutorId = 4, IsDeleted = false },
    new LibroAutor { Id = 5, LibroId = 5, AutorId = 5, IsDeleted = false },
    new LibroAutor { Id = 6, LibroId = 6, AutorId = 6, IsDeleted = false },
    new LibroAutor { Id = 7, LibroId = 7, AutorId = 7, IsDeleted = false },
    new LibroAutor { Id = 8, LibroId = 8, AutorId = 8, IsDeleted = false },
    new LibroAutor { Id = 9, LibroId = 9, AutorId = 9, IsDeleted = false },
    new LibroAutor { Id = 10, LibroId = 10, AutorId = 10, IsDeleted = false }
);
#endregion
#region datos semilla de 10 librogenero
modelBuilder.Entity<LibroGenero>().HasData(
    new LibroGenero { Id = 1, LibroId = 1, GeneroId = 1, IsDeleted = false },
    new LibroGenero { Id = 2, LibroId = 2, GeneroId = 2, IsDeleted = false },
    new LibroGenero { Id = 3, LibroId = 3, GeneroId = 3, IsDeleted = false },
    new LibroGenero { Id = 4, LibroId = 4, GeneroId = 4, IsDeleted = false },
    new LibroGenero { Id = 5, LibroId = 5, GeneroId = 5, IsDeleted = false },
    new LibroGenero { Id = 6, LibroId = 6, GeneroId = 6, IsDeleted = false },
    new LibroGenero { Id = 7, LibroId = 7, GeneroId = 7, IsDeleted = false },
    new LibroGenero { Id = 8, LibroId = 8, GeneroId = 8, IsDeleted = false },
    new LibroGenero { Id = 9, LibroId = 9, GeneroId = 9, IsDeleted = false },
    new LibroGenero { Id = 10, LibroId = 10, GeneroId = 10, IsDeleted = false }
);
#endregion
#region datos semilla de 10 usuariocarrera
modelBuilder.Entity<UsuarioCarrera>().HasData(
    new UsuarioCarrera { Id = 1, UsuarioId = 1, CarreraId = 1, IsDeleted = false },
    new UsuarioCarrera { Id = 2, UsuarioId = 2, CarreraId = 2, IsDeleted = false },
    new UsuarioCarrera { Id = 3, UsuarioId = 3, CarreraId = 3, IsDeleted = false },
    new UsuarioCarrera { Id = 4, UsuarioId = 4, CarreraId = 4, IsDeleted = false },
    new UsuarioCarrera { Id = 5, UsuarioId = 5, CarreraId = 5, IsDeleted = false },
    new UsuarioCarrera { Id = 6, UsuarioId = 6, CarreraId = 6, IsDeleted = false },
    new UsuarioCarrera { Id = 7, UsuarioId = 7, CarreraId = 7, IsDeleted = false },
    new UsuarioCarrera { Id = 8, UsuarioId = 8, CarreraId = 8, IsDeleted = false },
    new UsuarioCarrera { Id = 9, UsuarioId = 9, CarreraId = 9, IsDeleted = false },
    new UsuarioCarrera { Id = 10, UsuarioId = 10, CarreraId = 10, IsDeleted = false }
);
#endregion

            //configuramos los query filters para que no traigan los registros marcados como eliminados
            modelBuilder.Entity<Autor>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Genero>().HasQueryFilter(g => !g.IsDeleted);
            modelBuilder.Entity<Libro>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<Ejemplar>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => !u.IsDeleted);
            modelBuilder.Entity<Carrera>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Editorial>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Prestamo>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<UsuarioCarrera>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<LibroGenero>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<LibroAutor>().HasQueryFilter(e => !e.IsDeleted);













        }



    }
}
