using System.ComponentModel.DataAnnotations;

namespace MusicaNobaMVC.Models
{
    public class Album
    {
        [Key]
        public int IdAlbum { get; set; }
        [Required(ErrorMessage ="El Titulo de un album es obligatorio")]
        public string Titulo { get; set; }
        [Required]
        public int? Anio { get; set; }
        public ICollection<Cancion> Canciones { get; set; } = new List<Cancion>();

    }
}
