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
            Doctor doc = _doctor.getDoctor(HttpContext);
            var alerta = TempData["Mensaje"] as string;
            ViewBag.AlertMessage = alerta;
            return View(_doctor.getAllPacientes(doc.Iddoctor));
        }
        public IActionResult MasInfoPacientes(int IDPaciente)
        {
            return View(_doctor.getPaciente(IDPaciente));
        }
        public IActionResult RegistrarPacienteInvitado(int IdPaciente)
        {
            ViewBag.CurrentPage = "VerPacientes";
            Paciente paciente = _doctor.getPaciente(IdPaciente);
            paciente.Estado = 1;
            paciente.Constrasena = _doctor.generarClaveTemp();
            String cuerpo = _doctor.mensajeClave(paciente);
            _doctor.EnviarCorreo(paciente.Direccion, "Acceso a cuenta en Dientesitos", cuerpo);
            paciente.Constrasena = paciente.Constrasena;
            _doctor.updatePaciente(paciente);
            return RedirectToAction("VerPacientes"); ;
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
            String cuerpo = _doctor.mensajeClave(paciente);
            _doctor.EnviarCorreo(paciente.Direccion, "Acceso a cuenta en Dientesitos", cuerpo);
            paciente.Constrasena = paciente.Constrasena;
            _doctor.addPaciente(paciente);
            return RedirectToAction("VerPacientes");
        }
        public IActionResult VerCitas()
        {
            ViewBag.CurrentPage = "VerCitas";
            Doctor doc = _doctor.getDoctor(HttpContext);
            return View(_doctor.getCitas(doc.Iddoctor));
        }
        public IActionResult InfoCita(int IDCita)
        {
            return View(_doctor.getCita(IDCita));
        }
        public IActionResult FinalizarCita(int IDCita)
        {
            CitaDental cita = _doctor.getCita(IDCita);
            cita.Estado = 2;
            _doctor.editCita(cita);
            return RedirectToAction("VerCitas");
        }
        public IActionResult ReservarCita()
        {
            Doctor doc = _doctor.getDoctor(HttpContext);
            return View(_doctor.getDatosCita(doc.Iddoctor));
        }
        [HttpPost]
        public IActionResult ReservarCita(int idTratamiento, int idPaciente, int idSede, DateTime fecha, TimeSpan hora)
        {
            CitaDental cita = new CitaDental();
            Doctor doc = _doctor.getDoctor(HttpContext);
            cita.Idtratamiento = idTratamiento;
            cita.Iddoctor = doc.Iddoctor;
            cita.Idpaciente = idPaciente;
            cita.Idsede = idSede;
            cita.Fecha = fecha;
            cita.Hora = hora;
            cita.Duracion = 30;
            cita.ImportePagar = 300;
            cita.Estado = 0;

            _doctor.RegistrarCita(cita);
            return RedirectToAction("VerCitas");
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
