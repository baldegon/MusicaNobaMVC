using MusicaNobaMVC.Models;
using System.Composition.Convention;

namespace MusicaNobaMVC.ViewModels
{
    public class CancionListViewModel
    {
        public int IdCancion { get; set; }
        public string Nombre { get; set; }
        public string Artista { get; set; }
        public string AlbumTitulo { get; set; }
        public int AlbumId { get; set; }
        public string GeneroNombre { get; set; }
        public int GeneroId { get; set; }
    }
}
