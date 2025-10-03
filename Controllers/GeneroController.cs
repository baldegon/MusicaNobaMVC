using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MusicaNobaMVC.Data;
using MusicaNobaMVC.Models;
using MusicaNobaMVC.ViewModels;

namespace MusicaNobaMVC.Controllers
{
    public class GeneroController : Controller
    {

        private readonly ApplicationDbContext _context;

        public GeneroController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var genero = _context.Generos
                .Include(a => a.Canciones)
                .Select(a => new GeneroListViewModel
                {
                    IdGenero = a.IdGenero,
                    Nombre = a.Nombre,
                    CancionNombre = a.Canciones != null && a.Canciones.Any() ? a.Canciones.First().Nombre : string.Empty
                }).ToListAsync();

            return View(await genero);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        [Route("Genero/Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Nombre")] Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return View(genero);
            }

            _context.Add(genero);
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

            var genero = await _context.Generos
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.IdGenero == id);

            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdGenero,Nombre")] Genero genero)
        {
            if (id != genero.IdGenero)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(genero);
            }

            try
            {
                _context.Update(genero);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeneroExists(genero.IdGenero))
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

        private bool GeneroExists(int id)
        {
            return _context.Generos.Any(g => g.IdGenero == id);
        }
    }
}
