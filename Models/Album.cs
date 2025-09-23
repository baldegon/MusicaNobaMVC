using System.ComponentModel.DataAnnotations;

namespace MusicaNobaMVC.Models
{
    public class Album
    {
        [Key]
        public int IdAlbum { get; set; }
        public string Titulo { get; set; }
        public int? Anio { get; set; }
        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();

    }
}
