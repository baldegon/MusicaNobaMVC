using System.ComponentModel.DataAnnotations;

namespace MusicaNobaMVC.Models
{
    public class Cancion
    {
        [Key]
        public int IdCancion { get; set; }
        [Required(ErrorMessage = "El Nombre de un genero es obligatorio")]
        public string Nombre { get; set; }
        public string Artista { get; set; }
        public int AlbumId { get; set; }
        public Album? Album { get; set; }
        public int GeneroId { get; set; }
        public Genero? Genero { get; set; }
    }
}
