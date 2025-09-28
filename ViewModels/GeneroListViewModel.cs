using MusicaNobaMVC.Models;
using System.ComponentModel.DataAnnotations;


namespace MusicaNobaMVC.ViewModels
{
    public class GeneroListViewModel
    {
        public int IdGenero { get; set; }
        public string Nombre { get; set; }
        public string CancionNombre { get; set; }
    }
}
