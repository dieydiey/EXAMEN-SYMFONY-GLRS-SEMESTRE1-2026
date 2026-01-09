using Microsoft.AspNetCore.Mvc;
using inscription.Data;
using inscription.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace inscription.Controllers
{
    public class InscriptionController : Controller
    {
        private readonly IInscriptionServiceInterface _service;
        private readonly AppDbContext _context;

        public InscriptionController(
            IInscriptionServiceInterface service,
            AppDbContext context)
        {
            _service = service;
            _context = context;
        }

        public IActionResult Index(int classeId)
        {
            ViewBag.Classes = _context.Classes.ToList();
            
            var inscriptions = _service.ListerParClasse(classeId);
            
            return View(inscriptions);
        }

        public IActionResult Create()
        {
            ViewBag.Etudiants = _context.Etudiants.ToList();
            ViewBag.Classes = _context.Classes.ToList();
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int etudiantId, int classeId)
        {
            if (etudiantId > 0 && classeId > 0)
            {
                try 
                {
                    _service.Inscrire(etudiantId, classeId);
                    TempData["Success"] = "L'étudiant a été inscrit avec succès !";
                    return RedirectToAction(nameof(Index), new { classeId = classeId });
                }
                catch (InvalidOperationException ex) 
                {
                    ModelState.AddModelError("DoubleInscription", ex.Message);
                }
                catch (System.Exception ex)
                {
                    ModelState.AddModelError("", "Une erreur imprévue est survenue : " + ex.Message);
                }
            }

            ViewBag.Etudiants = _context.Etudiants.ToList();
            ViewBag.Classes = _context.Classes.ToList();
            return View();
        }
    }
}