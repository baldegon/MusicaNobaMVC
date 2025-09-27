using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicaNobaMVC.Data;
using MusicaNobaMVC.Models;

namespace MusicaNobaMVC.Controllers
{
    public class CancionesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CancionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Canciones
        public async Task<IActionResult> Index()
        {
            var data =  _context.Canciones
                       .Include(c => c.Album)
                       .Include(c => c.Genero)
                       .OrderBy(c => c.Nombre)
                       .ToList();

            return View(data);
        }

        // GET: Canciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await _context.Canciones
                .Include(c => c.Album)
                .Include(c => c.Genero)
                .FirstOrDefaultAsync(m => m.IdCancion == id);

            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // GET: Canciones/Create
        public async Task<IActionResult> Create()
        {
            //ViewData["AlbumId"] = new SelectList(await _context.Albums.OrderBy(a => a.Titulo).ToListAsync(), "IdAlbum", "Titulo");
            ViewData["GeneroId"] = new SelectList(await _context.Generos.OrderBy(g => g.Nombre).ToListAsync(), "IdGenero", "Nombre");

            return View(new Cancion());
        }

        // POST: Canciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCancion,Nombre,Artista,Album,AlbumId,Genero,GeneroId")] Cancion cancion)
        {
            if (!ModelState.IsValid)
            {
                //ViewData["AlbumId"] = new SelectList(await _context.Albums.OrderBy(a => a.Titulo).ToListAsync(), "IdAlbum", "Titulo");
                ViewData["GeneroId"] = new SelectList(await _context.Generos.OrderBy(g => g.Nombre).ToListAsync(), "IdGenero", "Nombre");
                //ViewData["AlbumId"] = new SelectList(_context.Albums, "IdAlbum", "Titulo", cancion.AlbumId);
                //ViewData["GeneroId"] = new SelectList(_context.Generos, "IdGenero", "Nombre", cancion.GeneroId);
                return View(cancion);       // OK: Cancion -> Create.cshtml (Cancion)
            }

            _context.Add(cancion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Canciones/Edit/5
        public async Task<IActionResult> Edit([Bind("IdCancion,Nombre,Artista,Album,AlbumId,Genero,GeneroId")] int? id)
        {
            var cancion = await _context.Canciones.FindAsync(id);

                //ViewData["AlbumId"] = new SelectList(await _context.Albums.OrderBy(a => a.Titulo).ToListAsync(), "IdAlbum", "Titulo");
                ViewData["GeneroId"] = new SelectList(await _context.Generos.OrderBy(g => g.Nombre).ToListAsync(), "IdGenero", "Nombre");
                //ViewData["AlbumId"] = new SelectList(_context.Albums, "IdAlbum", "Titulo", cancion.AlbumId);
                //ViewData["GeneroId"] = new SelectList(_context.Generos, "IdGenero", "Nombre", cancion.GeneroId);
            
            return View(cancion);
        }

        // POST: Canciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCancion,Nombre,Artista,AlbumId,GeneroId")] Cancion cancion)
        {
            if (id != cancion.IdCancion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //ViewData["Album"] = new SelectList(_context.Albums, "IdAlbum", "Titulo", cancion.AlbumId);
                    //ViewData["Genero"] = new SelectList(_context.Generos, "IdGenero", "Nombre", cancion.GeneroId);
                    ViewData["AlbumId"] = new SelectList(await _context.Albums.OrderBy(a => a.Titulo).ToListAsync(), "IdAlbum", "Titulo");
                    ViewData["GeneroId"] = new SelectList(await _context.Generos.OrderBy(g => g.Nombre).ToListAsync(), "IdGenero", "Nombre");
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

            
            return View(new Cancion());
        }

        // GET: Canciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cancion = await _context.Canciones
                .Include(c => c.Album)
                .Include(c => c.Genero)
                .FirstOrDefaultAsync(m => m.IdCancion == id);
            if (cancion == null)
            {
                return NotFound();
            }

            return View(cancion);
        }

        // POST: Canciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var cancion = await _context.Canciones.FindAsync(id);
            if (cancion != null)
            {
                _context.Canciones.Remove(cancion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CancionExists(int id)
        {
            return _context.Canciones.Any(e => e.IdCancion == id);
        }



    }
}
