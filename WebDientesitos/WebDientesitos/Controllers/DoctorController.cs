using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly IDoctor _doctor;
        public DoctorController(IDoctor doctor)
        {
            _doctor = doctor;
        }
        public IActionResult MenuDoctor()
        {
            ViewBag.CurrentPage = "MenuDoctor";
            Doctor doc = _doctor.getDoctor(HttpContext);

            return View(doc);
        }
        public IActionResult VerPacientes()
        {
            ViewBag.CurrentPage = "VerPacientes";
            var alerta = TempData["Mensaje"] as string;
            ViewBag.AlertMessage = alerta;
            return View();
        }
        public IActionResult RegistrarPaciente()
        {
            ViewBag.CurrentPage = "VerPacientes";
            return View();
        }
        [HttpPost]
        public IActionResult RegistrarPaciente(Paciente paciente)
        {
            ViewBag.CurrentPage = "VerPacientes";
            paciente.Estado = 1;
            paciente.Constrasena = _doctor.generarClaveTemp();
            //String cuerpo = _doctor.mensajeClave(paciente);
            //_doctor.EnviarCorreo(paciente.Direccion, "Acceso a cuenta en Dientesitos", cuerpo);
            TempData["Mensaje"] = paciente.Constrasena;
            paciente.Constrasena = _doctor.convertirSha256(paciente.Constrasena);
            _doctor.addPaciente(paciente);
            return RedirectToAction("VerPacientes","Doctor");
        }
        public IActionResult PacienteRegistrado(Paciente paciente)
        {
            return View(paciente);
        }
        public IActionResult VerCitas()
        {
            ViewBag.CurrentPage = "VerCitas";
            return View();
        }
        public IActionResult EditarPerfil()
        {
            ViewBag.CurrentPage = "EditarPerfil";
            Doctor doc = _doctor.getDoctor(HttpContext);
            return View(doc);
        }
        [HttpPost]
        public IActionResult EditarPerfil(Doctor docEdit)
        {
            ViewBag.CurrentPage = "EditarPerfil";
            Doctor doc = _doctor.getDoctor(HttpContext);
            doc.Nombre = docEdit.Nombre;
            doc.ApellidoPaterno = docEdit.ApellidoPaterno;
            doc.ApellidoMaterno = docEdit.ApellidoPaterno;
            doc.NumeroColegioMedico = docEdit.NumeroColegioMedico;
            _doctor.editDoctor(doc);
            return View(doc);
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
            Doctor doc = _doctor.getDoctor(HttpContext);
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
            doc.Constrasena = _doctor.convertirSha256(contrasena);
            _doctor.editDoctor(doc);
            return RedirectToAction("EditarPerfil", "Doctor");
        }
    }
}
