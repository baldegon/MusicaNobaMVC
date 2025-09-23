using System.ComponentModel.DataAnnotations;

namespace MusicaNobaMVC.Models
{
    public class Genero
    {
        [Key]
        public int IdGenero { get; set; }
        public string Nombre { get; set; }
        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();
    }
}
