using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using MusicaNobaMVC.Data;
using MusicaNobaMVC.Models;
using MusicaNobaMVC.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MusicaNobaMVC.Controllers
{
    public class CancionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CancionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Cancion> canciones = new List<Cancion>();

        public List<Album> albumes = new List<Album>();

        public List<Genero> generos = new List<Genero>();

        /*
         * Metodo GET para retornar las canciones al INDEX
         * utilizo include para traer los albumes y generos, que estan relacionados a
         * canciones.
         * Con los datos obtenidos, los mando al viewmodel para que este los envie a la View
        */
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var canciones = _context.Canciones
                .Include(c => c.Album)
                .Include(c => c.Genero)
                .Select(c => new CancionListViewModel
                {
                    IdCancion = c.IdCancion,
                    Nombre = c.Nombre,
                    Artista = c.Artista,
                    AlbumTitulo = c.Album != null ? c.Album.Titulo : string.Empty,
                    GeneroNombre = c.Genero != null ? c.Genero.Nombre : string.Empty
                }).ToListAsync();

            return View( await canciones);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Nombre,Artista,AlbumId,GeneroId")] Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                return View(cancion);
            }

            _context.Add(cancion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await _context.Canciones
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.IdCancion == id);

            if (cancion == null)
            {
                return NotFound();
            }

            await LoadDropDownDataAsync(cancion.AlbumId, cancion.GeneroId);

            return View(cancion);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCancion,Nombre,Artista,AlbumId,GeneroId")] Cancion cancion)
        {
            if (id != cancion.IdCancion)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                await LoadDropDownDataAsync(cancion.AlbumId, cancion.GeneroId);
                return View(cancion);
            }

            try
            {
                _context.Update(cancion);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CancionExists(cancion.IdCancion))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadDropDownDataAsync(int? albumId = null, int? generoId = null)
        {
            var albumes = await _context.Albums
                .AsNoTracking()
                .ToListAsync();
            var generos = await _context.Generos
                .AsNoTracking()
                .ToListAsync();

            ViewData["AlbumId"] = new SelectList(albumes, nameof(Album.IdAlbum), nameof(Album.Titulo), albumId);
            ViewData["GeneroId"] = new SelectList(generos, nameof(Genero.IdGenero), nameof(Genero.Nombre), generoId);
        }

        private bool CancionExists(int id)
        {
            return _context.Canciones.Any(e => e.IdCancion == id);
        }

    }
}
