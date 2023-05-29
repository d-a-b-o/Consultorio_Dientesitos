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
        public PacienteController(IPaciente paciente)
        {
            _paciente = paciente;
        }
        public IActionResult MenuPaciente()
        {
            ViewBag.CurrentPage = "MenuPaciente";
            Paciente paciente = _paciente.getPaciente(HttpContext);

            return View(paciente);
        }
        public IActionResult VerCitas()
        {
            ViewBag.CurrentPage = "VerCitas";
            Paciente paciente = _paciente.getPaciente(HttpContext);
            return View(_paciente.getCitas(paciente.Idpaciente));
        }
        public IActionResult VerHistorial()
        {
            ViewBag.CurrentPage = "VerHistorial";
            Paciente paciente = _paciente.getPaciente(HttpContext);
            return View(_paciente.getCitasFin(paciente.Idpaciente));
        }
        public IActionResult InfoCita(int IDCita)
        {
            return View(_paciente.getCitaDental(IDCita));
        }
        public IActionResult EditarPerfil()
        {
            ViewBag.CurrentPage = "EditarPerfil";
            Paciente paciente = _paciente.getPaciente(HttpContext);
            return View(paciente);
        }
        [HttpPost]
        public IActionResult EditarPerfil(Paciente pacienteEdit)
        {
            ViewBag.CurrentPage = "EditarPerfil";
            Paciente paciente = _paciente.getPaciente(HttpContext);
            paciente.Nombre = pacienteEdit.Nombre;
            paciente.ApellidoPaterno = pacienteEdit.ApellidoPaterno;
            paciente.ApellidoMaterno = pacienteEdit.ApellidoMaterno;
            paciente.Direccion = pacienteEdit.Direccion;
            paciente.Telefono = pacienteEdit.Telefono;
            paciente.Edad = pacienteEdit.Edad;
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
            Paciente paciente = _paciente.getPaciente(HttpContext);
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
            paciente.Constrasena = _paciente.convertirSha256(contrasena);
            _paciente.editPaciente(paciente);
            return RedirectToAction("EditarPerfil", "Paciente");
        }
        public IActionResult ReservarCita()
        {
            return View(_paciente.getDatosCita());
        }
        [HttpPost]
        public IActionResult ReservarCita(int sedeCod, int tratamientoCod, int doctorCod, String fecha, String hora)
        {
            Paciente paciente = _paciente.getPaciente(HttpContext);
            _paciente.addCita(sedeCod, tratamientoCod, doctorCod, paciente.Idpaciente, fecha, hora);
            return RedirectToAction("VerCitas");
        }
        public IActionResult EditarCita(int citaId)
        {
            if (_paciente.CompararFechas((DateTime)_paciente.getCitaDental(citaId).Fecha))
            {
                ViewData["Mensaje"] = "Fecha";
            }
            return View(_paciente.getDatosCita(citaId));
        }
        [HttpPost]
        public IActionResult EditarCita(CitaDental citaEdit)
        {
            CitaDental citaOrigin = _paciente.getCitaDental(citaEdit.Idcita);
            citaOrigin.Fecha = citaEdit.Fecha;
            citaOrigin.Hora = citaEdit.Hora;
            _paciente.editCita(citaOrigin);
            return RedirectToAction("EditarCita", citaEdit.Idcita);
        }
        public IActionResult CancelarCita(int IDCita)
        {
            CitaDental cita = _paciente.getCitaDental(IDCita);
            cita.Estado = 3;
            _paciente.editCita(cita);
            return RedirectToAction("VerCitas");
        }
    }
}
