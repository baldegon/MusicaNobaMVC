using Microsoft.Build.ObjectModelRemoting;
using MusicaNobaMVC.Models;
using System.ComponentModel.DataAnnotations;

namespace MusicaNobaMVC.ViewModels
{
    public class AlbumListViewModel
    {
        public int IdAlbum { get; set; }
        public string Titulo { get; set; }
        public int? Anio { get; set; }
        public string CancionNombre { get; set; }

    }
}
