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
        private readonly IPaciente _paciente;

        public HomeController(ILogger<HomeController> logger, ICita cita, IPaciente paciente)
        {
            _logger = logger;
            _cita = cita;
            _paciente = paciente;
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
            return View(_cita.getDatosCita());
        }
        public IActionResult ConfirmacionCita(CitaSinUser citaSinUser, int idSede, int idDoctor, int idTratamiento)
        {
            var paciente = new Paciente();
            paciente.Documento = citaSinUser.DniPaciente;
            if (!_paciente.datosPacienteExisten(paciente))
            {
                citaSinUser.IdSede = idSede;
                citaSinUser.IdDoctor = idDoctor;
                citaSinUser.IdTratamiento = idTratamiento;
                CitaDental citaTemp = _cita.prepareCitaSinUser(citaSinUser);
                _cita.registrarCita(citaTemp);
                return RedirectToAction("Inicio");
            }
            ViewData["Mensaje"] = "dni ya registrado";
            return RedirectToAction("Cita");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}