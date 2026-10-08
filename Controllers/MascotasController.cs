using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VetCare.Data;
using VetCare.Models;

namespace VetCare.Controllers;

public class MascotasController : Controller
{
    private readonly VetCareContext _context;

    public MascotasController(VetCareContext context)
    {
        _context = context;
    }

    // GET: Mascotas  (RF-05: búsqueda por nombre o especie con LINQ)
    public async Task<IActionResult> Index(string? search)
    {
        ViewBag.Search = search;

        var query = _context.Mascotas.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim().ToLower();
            query = query.Where(m =>
                m.Nombre.ToLower().Contains(term) ||
                m.Especie.ToLower().Contains(term));
        }

        var mascotas = await query
            .OrderBy(m => m.Nombre)
            .ToListAsync();

        return View(mascotas);
    }

    // GET: Mascotas/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
            return NotFound();

        var mascota = await _context.Mascotas
            .FirstOrDefaultAsync(m => m.IdMascota == id);

        if (mascota == null)
            return NotFound();

        return View(mascota);
    }

    // GET: Mascotas/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Mascotas/Create  (RF-04: validaciones)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Nombre,Especie,Raza,FechaNacimiento,NombrePropietario,TelefonoPropietario")]
        Mascota mascota)
    {
        if (ModelState.IsValid)
        {
            _context.Add(mascota);
            await _context.SaveChangesAsync();
            TempData["Success"] = $"Mascota '{mascota.Nombre}' registrada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        return View(mascota);
    }

    // GET: Mascotas/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
            return NotFound();

        var mascota = await _context.Mascotas.FindAsync(id);
        if (mascota == null)
            return NotFound();

        return View(mascota);
    }

    // POST: Mascotas/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
        int id,
        [Bind("IdMascota,Nombre,Especie,Raza,FechaNacimiento,NombrePropietario,TelefonoPropietario")]
        Mascota mascota)
    {
        if (id != mascota.IdMascota)
            return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(mascota);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Mascota '{mascota.Nombre}' actualizada correctamente.";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Mascotas.AnyAsync(m => m.IdMascota == id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(mascota);
    }

    // GET: Mascotas/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
            return NotFound();

        var mascota = await _context.Mascotas
            .Include(m => m.Consultas)
            .FirstOrDefaultAsync(m => m.IdMascota == id);

        if (mascota == null)
            return NotFound();

        return View(mascota);
    }

    // POST: Mascotas/Delete/5  (RF-04: no eliminar si tiene consultas)
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var mascota = await _context.Mascotas
            .Include(m => m.Consultas)
            .FirstOrDefaultAsync(m => m.IdMascota == id);

        if (mascota == null)
            return NotFound();

        if (mascota.Consultas.Any())
        {
            TempData["Error"] =
                $"No se puede eliminar '{mascota.Nombre}' porque tiene " +
                $"{mascota.Consultas.Count} consulta(s) registrada(s). " +
                "Elimine primero el historial médico.";
            return RedirectToAction(nameof(Index));
        }

        _context.Mascotas.Remove(mascota);
        await _context.SaveChangesAsync();
        TempData["Success"] = $"Mascota '{mascota.Nombre}' eliminada correctamente.";
        return RedirectToAction(nameof(Index));
    }
}
