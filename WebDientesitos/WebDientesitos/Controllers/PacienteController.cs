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
            return View();
        }
        public IActionResult VerHistorial()
        {
            ViewBag.CurrentPage = "VerHistorial";
            return View();
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
    }
}
