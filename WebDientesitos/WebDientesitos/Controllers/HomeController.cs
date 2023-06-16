using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICita _cita;

        public HomeController(ILogger<HomeController> logger, ICita cita)
        {
            _logger = logger;
            _cita = cita;
        }

        public IActionResult Inicio()
        {
            ViewBag.CurrentPage = "Inicio";
            return View();
        }
        public IActionResult Nosotros()
        {
            ViewBag.CurrentPage = "Nosotros";
            return View();
        }
        public IActionResult Servicios()
        {
            ViewBag.CurrentPage = "Servicios";
            return View();
        }
        public IActionResult Cita()
        {
            ViewBag.CurrentPage = "Cita";
            return View(_cita.GetDatosCita());
        }
        public IActionResult ConfirmacionCita(CitaSinUser citaSinUser, int idSede, int idDoctor, int idTratamiento)
        {
            citaSinUser.IdSede = idSede;
            citaSinUser.IdDoctor = idDoctor;
            citaSinUser.IdTratamiento = idTratamiento;
            CitaDental citaTemp = _cita.prepareCitaSinUser(citaSinUser);
            _cita.registrarCita(citaTemp);
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}