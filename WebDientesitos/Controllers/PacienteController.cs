using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        private readonly IPaciente _paciente;
        private readonly ICita _cita;
        private readonly IUtility _utility;
        public PacienteController(IPaciente paciente, ICita cita, IUtility utility)
        {
            _paciente   = paciente;
            _cita       = cita;
            _utility    = utility; 
        }
        public IActionResult MenuPaciente()
        {
            ViewBag.CurrentPage = "MenuPaciente";
            var paciente        = _paciente.getPaciente(HttpContext);

            return View(paciente);
        }
        public IActionResult VerCitas()
        {
            ViewBag.CurrentPage = "VerCitas";
            var paciente        = _paciente.getPaciente(HttpContext);

            return View(_cita.getCitasPendientesXPaciente(paciente.Idpaciente));
        }
        public IActionResult VerHistorial()
        {
            ViewBag.CurrentPage = "VerHistorial";
            var paciente        = _paciente.getPaciente(HttpContext);

            return View(_cita.getCitasFinalizadasXPaciente(paciente.Idpaciente));
        }
        public IActionResult InfoCita(int IDCita)
        {
            return View(_cita.getCitaDentalXId(IDCita));
        }
        public IActionResult EditarPerfil()
        {
            ViewBag.CurrentPage = "EditarPerfil";
            var paciente        = _paciente.getPaciente(HttpContext);

            return View(paciente);
        }
        [HttpPost]
        public IActionResult EditarPerfil(Paciente pacienteEdit)
        {
            ViewBag.CurrentPage = "EditarPerfil";
            var paciente = _paciente.getPaciente(HttpContext);

            paciente.Nombre             = pacienteEdit.Nombre;
            paciente.ApellidoPaterno    = pacienteEdit.ApellidoPaterno;
            paciente.ApellidoMaterno    = pacienteEdit.ApellidoMaterno;
            paciente.Direccion          = pacienteEdit.Direccion;
            paciente.Telefono           = pacienteEdit.Telefono;
            paciente.Edad               = pacienteEdit.Edad;
            _paciente.editPaciente(paciente);

            return View(paciente);
        }
        public IActionResult EditarContrasena()
        {
            ViewBag.CurrentPage = "EditarPerfil";

            return View();
        }
        [HttpPost]
        public IActionResult EditarContrasena(String contrasena, String contrasenaConfi)
        {
            ViewBag.CurrentPage = "EditarPerfil";
            var paciente        = _paciente.getPaciente(HttpContext);

            if (contrasena.Length < 8)
            {
                ViewData["Mensaje"] = "tamaño";
                return View();
            }
            if (!contrasena.Equals(contrasenaConfi))
            {
                ViewData["Mensaje"] = "Coincidencia";
                return View();
            }
            paciente.Constrasena = _utility.convertirSha256(contrasena);
            _paciente.editPaciente(paciente);

            return RedirectToAction("EditarPerfil", "Paciente");
        }
        public IActionResult ReservarCita()
        {
            return View(_cita.getDatosCita());
        }
        [HttpPost]
        public IActionResult ReservarCita(int sedeCod, int tratamientoCod, int doctorCod, String fecha, String hora)
        {
            var paciente    = _paciente.getPaciente(HttpContext);
            var cita        = _cita.createCitaDental(sedeCod, tratamientoCod, doctorCod, paciente.Idpaciente, fecha, hora);

            _cita.registrarCita(cita);

            return RedirectToAction("VerCitas");
        }
        public IActionResult EditarCita(int citaId)
        {
            if (_paciente.CompararFechas((DateTime)_cita.getCitaDentalXId(citaId).Fecha))
            {
                ViewData["Mensaje"] = "Fecha";
            }
            return View(_cita.getDatosCita(citaId));
        }
        [HttpPost]
        public IActionResult EditarCita(int citaId, String fecha, String hora)
        {
            var citaEdit    = _cita.getCitaDentalXId(citaId);

            citaEdit.Fecha  = DateTime.Parse(fecha);
            citaEdit.Hora   = TimeSpan.Parse(hora);
            _cita.editCita(citaEdit);

            return RedirectToAction("VerCitas");
        }
        public IActionResult CancelarCita(int IDCita)
        {
            var cita = _cita.getCitaDentalXId(IDCita);

            cita.Estado = 3;
            _cita.editCita(cita);

            return RedirectToAction("VerCitas");
        }
    }
}
