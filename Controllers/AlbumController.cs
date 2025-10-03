using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
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

        [AllowAnonymous]
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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        [Route("Album/Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Titulo,Anio")] Album album)
        {
            if (!ModelState.IsValid)
            {
                return View(album)  ;
            }

            _context.Add(album);
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

            var album = await _context.Albums
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.IdAlbum == id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Album album)
        {
            if (id != album.IdAlbum)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(album);
            }

            try
            {
                _context.Update(album);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(album.IdAlbum))
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

        private bool AlbumExists(int id)
        {
            return _context.Albums.Any(a => a.IdAlbum == id);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var album = await _context.Albums
                .Include(a => a.Canciones)
                    .ThenInclude(c => c.Genero)
                .Include(a => a.Canciones)
                .FirstOrDefaultAsync(m => m.IdAlbum == id);

            if (album == null) return NotFound();

            return View(album);
        }
    }
}
