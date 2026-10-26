using GestionF1.Web.EF;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GestionF1.Web.Controllers
    {

    public class PilotoController : Controller
        {

        private readonly Formula1Context _context;

        public PilotoController()
            {

            this._context = new Formula1Context();
            }

        [HttpGet]
        public IActionResult AgregarPiloto()
            {

            ViewBag.Escuderias = new SelectList(this._context.Escuderia.OrderBy(e => e.NombreEscuderia), "IdEscuderia", "NombreEscuderia");
            return View("Agregar");
            }


        [HttpPost]
        public IActionResult AgregarPiloto(string NombrePiloto, int IdEscuderia)
            {

            Piloto piloto = new Piloto();

            piloto.NombrePiloto = NombrePiloto;
            piloto.IdEscuderia = IdEscuderia;
            piloto.Eliminado = false;

            this._context.Pilotos.Add(piloto);
            this._context.SaveChanges();

            return RedirectToAction("ListarPilotos");
            }

        public IActionResult ListarPilotos(int? idEscuderia)
            {

            ViewBag.Escuderias = new SelectList(this._context.Escuderia.OrderBy(e => e.NombreEscuderia), "IdEscuderia", "NombreEscuderia", idEscuderia);

            var pilotos = this._context.Pilotos.Include(p => p.IdEscuderiaNavigation).Where(p => p.Eliminado == false);

            if (idEscuderia != null)
                {

                pilotos = pilotos.Where(p => p.IdEscuderia == idEscuderia);
                }

            return View(pilotos.ToList());
            }

        [HttpPost]
        public IActionResult EliminarPiloto(int id)
            {
            Piloto piloto = this._context.Pilotos.Find(id);

            if (piloto != null)
                {
                piloto.Eliminado = true;
                this._context.SaveChanges();
                }

            return RedirectToAction("ListarPilotos");
            }
        }
    }