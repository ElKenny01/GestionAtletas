using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using SIGRAUM2025.Data;
using SIGRAUM2025.Models;

namespace SIGRAUM2025.Controllers
{
    public class ParticipacionEventoesController : Controller
    {
        private readonly SIGRAUM2025Context _context;

        public ParticipacionEventoesController(SIGRAUM2025Context context)
        {
            _context = context;
        }

        // GET: ParticipacionEventoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParticipacionEvento.ToListAsync());
        }

        // GET: ParticipacionEventoes/Asignar
        public IActionResult Asignar(int idEvento, string nombreEvento)
        {
            ViewBag.IdEvento = idEvento;
            ViewBag.NombreEvento = nombreEvento;
            return View();
        }

        // POST: ParticipacionEventoes/Asignar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Asignar(int idEvento, string nombreEvento)
        {
            var accionParam = new SqlParameter("@Accion", 5);
            var atletas = await _context.Atleta
                .FromSqlRaw("EXEC dbo.sp_ParticipacionEvento @Accion", accionParam)
                .ToListAsync();

            ViewBag.IdEvento = idEvento;
            ViewBag.NombreEvento = nombreEvento;
            return View("AsignarParticipante", atletas);
        }

        // POST: ParticipacionEventoes/CreateWithSP
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateWithSP(int idEvento, List<int> selectedAtletas, string? resultado)
        {
            if (selectedAtletas == null || selectedAtletas.Count == 0)
            {
                ModelState.AddModelError("", "Seleccione al menos un atleta");
                return RedirectToAction(nameof(Index));
            }

            foreach (var atletaId in selectedAtletas)
            {
                var accionParam = new SqlParameter("@Accion", 1);
                var idAtletaParam = new SqlParameter("@IdAtleta", atletaId);
                var idEventoParam = new SqlParameter("@IdEvento", idEvento);
                var resultadoParam = new SqlParameter("@Resultado", (object?)resultado ?? DBNull.Value);

                await _context.Database.ExecuteSqlRawAsync(
                    "EXEC dbo.sp_ParticipacionEvento @Accion, NULL, @IdAtleta, @IdEvento, @Resultado",
                    accionParam, idAtletaParam, idEventoParam, resultadoParam);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
