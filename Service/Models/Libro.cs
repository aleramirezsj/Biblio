using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class Libro
    {
        public int Id { get; set; }
        [Required]
        public string Titulo { get; set; }= string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int EditorialId { get; set; } =1;
        public Editorial? Editorial { get; set; }
        [Required]
        public int Paginas { get; set; } = 0;
        public int AnioPublicacion { get; set; } = 0;
        public string Portada { get; set; } = string.Empty;
        [Required]
        [Column(TypeName = "text")]
        public string Sinopsis { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        virtual public ICollection<Autor> Autores { get; set; } = new List<Autor>();
        virtual public ICollection<Genero> Generos { get; set; } = new List<Genero>();


        public override string ToString()
        {
            return Titulo;
        }
    }
}
