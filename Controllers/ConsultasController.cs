using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetCare.Data;
using VetCare.Models;

namespace VetCare.Controllers;

public class ConsultasController : Controller
{
    private readonly VetCareContext _context;

    public ConsultasController(VetCareContext context)
    {
        _context = context;
    }

    // GET: Consultas/Create?mascotaId=5  (RF-02)
    public async Task<IActionResult> Create(int? mascotaId)
    {
        // RF-04: no registrar consultas para mascotas inexistentes
        Mascota? mascota = null;
        if (mascotaId.HasValue)
        {
            mascota = await _context.Mascotas.FindAsync(mascotaId.Value);
            if (mascota == null)
            {
                TempData["Error"] = "La mascota seleccionada no existe.";
                return RedirectToAction("Index", "Mascotas");
            }
        }

        ViewBag.Mascota = mascota;
        ViewBag.MascotasSelectList = new SelectList(
            await _context.Mascotas.OrderBy(m => m.Nombre).ToListAsync(),
            "IdMascota", "Nombre",
            mascotaId);

        var consulta = new Consulta
        {
            IdMascota = mascotaId ?? 0,
            FechaConsulta = DateTime.Today
        };

        return View(consulta);
    }

    // POST: Consultas/Create  (RF-04: no fechas futuras)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("IdMascota,FechaConsulta,Motivo,Diagnostico,Tratamiento")]
        Consulta consulta)
    {
        // RF-04: fecha no puede ser futura
        if (consulta.FechaConsulta > DateTime.Now)
            ModelState.AddModelError("FechaConsulta",
                "La fecha de la consulta no puede ser una fecha futura.");

        // RF-04: mascota debe existir
        var mascota = await _context.Mascotas.FindAsync(consulta.IdMascota);
        if (mascota == null)
            ModelState.AddModelError("IdMascota",
                "La mascota seleccionada no existe.");

        if (ModelState.IsValid)
        {
            _context.Add(consulta);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Consulta registrada correctamente.";
            return RedirectToAction("Historial", new { mascotaId = consulta.IdMascota });
        }

        ViewBag.Mascota = mascota;
        ViewBag.MascotasSelectList = new SelectList(
            await _context.Mascotas.OrderBy(m => m.Nombre).ToListAsync(),
            "IdMascota", "Nombre",
            consulta.IdMascota);

        return View(consulta);
    }

    // GET: Consultas/Historial/5  (RF-03: historial ordenado desc)
    public async Task<IActionResult> Historial(int? mascotaId)
    {
        if (mascotaId == null)
            return RedirectToAction("Index", "Mascotas");

        var mascota = await _context.Mascotas
            .Include(m => m.Consultas)
            .FirstOrDefaultAsync(m => m.IdMascota == mascotaId);

        if (mascota == null)
            return NotFound();

        // RF-03: ordenadas por fecha descendente
        mascota.Consultas = mascota.Consultas
            .OrderByDescending(c => c.FechaConsulta)
            .ToList();

        return View(mascota);
    }
}
