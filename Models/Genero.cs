using System.ComponentModel.DataAnnotations;

namespace MusicaNobaMVC.Models
{
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }
        [Required(ErrorMessage = "El Nombre de un genero es obligatorio")]
        public string Nombre { get; set; }
        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();
    }
}
