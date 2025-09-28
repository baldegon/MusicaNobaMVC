using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
