using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Genero
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
    }
}
