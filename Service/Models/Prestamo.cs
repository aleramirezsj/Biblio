using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models
{
    public class Prestamo
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
        public int EjemplarId { get; set; }
        public Ejemplar? Ejemplar { get; set; }
        public DateTimeOffset FechaPrestamo { get; set; } = DateTime.UtcNow;
        public DateTimeOffset? FechaDevolucion { get; set; }
        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return $"{Ejemplar?.Libro?.Titulo} - {FechaPrestamo.ToString("dd/MM/yyyy")} - {FechaDevolucion?.ToString("dd/MM/yyyy")}";
        }
    }
}
