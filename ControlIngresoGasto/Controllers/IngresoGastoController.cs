using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ControlIngresoGasto.Models;
using ControlIngresoGasto.datas;

namespace ControlIngresoGasto.Controllers
{
    public class IngresoGastoController : Controller
    {
        private readonly AplicationDbContext _context;

        public IngresoGastoController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: IngresoGasto
        public async Task<IActionResult> Index(int? mes, int? anio)
        {
            if(mes == null)
            {
                mes = DateTime.Now.Month;
            }
            if(anio == null)
            {
                anio = DateTime.Now.Year;
            }

            ViewData["mes"] = mes;
            ViewData["anio"] = anio;

            var aplicationDbContext = _context.IngresoGasto.Include(i => i.Categoria)
                .Where(i=> i.Fecha.Month==mes && i.Fecha.Year==anio);
            
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: IngresoGasto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrseoGasto = await _context.IngresoGasto
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrseoGasto == null)
            {
                return NotFound();
            }

            return View(ingrseoGasto);
        }

        // GET: IngresoGasto/Create
        public IActionResult Create()
        {
            ViewData["CategoriaId"] = new SelectList(_context.Categorias.Where(v=>v.Estado==true), "Id", "NombreCateroria");
            return View();
        }

        // POST: IngresoGasto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoriaId,Fecha,Valor")] IngrseoGasto ingrseoGasto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingrseoGasto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCateroria", ingrseoGasto.CategoriaId);
            return View(ingrseoGasto);
        }

        // GET: IngresoGasto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrseoGasto = await _context.IngresoGasto.FindAsync(id);
            if (ingrseoGasto == null)
            {
                return NotFound();
            }
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCateroria", ingrseoGasto.CategoriaId);
            return View(ingrseoGasto);
        }

        // POST: IngresoGasto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CategoriaId,Fecha,Valor")] IngrseoGasto ingrseoGasto)
        {
            if (id != ingrseoGasto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrseoGasto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngrseoGastoExists(ingrseoGasto.Id))
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
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "Id", "NombreCateroria", ingrseoGasto.CategoriaId);
            return View(ingrseoGasto);
        }

        // GET: IngresoGasto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrseoGasto = await _context.IngresoGasto
                .Include(i => i.Categoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrseoGasto == null)
            {
                return NotFound();
            }

            return View(ingrseoGasto);
        }

        // POST: IngresoGasto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrseoGasto = await _context.IngresoGasto.FindAsync(id);
            _context.IngresoGasto.Remove(ingrseoGasto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngrseoGastoExists(int id)
        {
            return _context.IngresoGasto.Any(e => e.Id == id);
        }
    }
}
