using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicaNobaMVC.Data;
using MusicaNobaMVC.Models;
using MusicaNobaMVC.ViewModels;

namespace MusicaNobaMVC.Controllers
{
    public class AlbumController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlbumController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var album = _context.Albums
                .Include(a => a.Canciones)
                .Select(a => new AlbumListViewModel
                {
                    IdAlbum = a.IdAlbum,
                    Titulo = a.Titulo,
                    Anio = a.Anio,
                    CancionNombre = a.Canciones != null && a.Canciones.Any() ? a.Canciones.First().Nombre : string.Empty
                }).ToListAsync();

            return View(await album);
        }

    }
}
