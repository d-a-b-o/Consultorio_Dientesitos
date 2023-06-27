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
        private readonly IDoctor    _doctor;
        private readonly ICita      _cita;
        private readonly IPaciente  _paciente;
        private readonly IReporte   _reporte;
        private readonly IUtility   _utility;
        public DoctorController(IDoctor doctor, ICita cita, IPaciente paciente, IUtility utility, IReporte reporte)
        {
            _doctor     = doctor;
            _cita       = cita;
            _paciente   = paciente;
            _utility    = utility;
            _reporte    = reporte;
        }
        public IActionResult MenuDoctor()
        {
            ViewBag.CurrentPage = "MenuDoctor";
            var doc             = _doctor.getDoctor(HttpContext);

            return View(doc);
        }
        public IActionResult VerPacientes()
        {
            ViewBag.CurrentPage     = "VerPacientes";
            var doc                 = _doctor.getDoctor(HttpContext);
            ViewBag.AlertMessage    = TempData["Mensaje"] as string;

            return View(_paciente.getPacientesXDoctor(doc.Iddoctor));
        }
        public IActionResult MasInfoPacientes(int IDPaciente)
        {
            return View(_paciente.getPacienteXId(IDPaciente));
        }
        public IActionResult RegistrarPacienteInvitado(int IdPaciente)
        {
            ViewBag.CurrentPage = "VerPacientes";
            var paciente        = _paciente.getPacienteXId(IdPaciente);

            paciente.Estado         = 1;
            paciente.Constrasena    = _utility.generarClaveTemporal();
            _utility.enviarCorreo(paciente.Direccion, "Acceso a cuenta en Dientesitos", _utility.mensajeCLaveCreada(paciente));
            paciente.Constrasena    = paciente.Constrasena;
            _paciente.editPaciente(paciente);

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
            ViewBag.CurrentPage     = "VerPacientes";

            if (!_paciente.datosPacienteExisten(paciente))
            {
                paciente.Estado = 1;
                paciente.Constrasena = _utility.generarClaveTemporal();
                _utility.enviarCorreo(paciente.Direccion, "Acceso a cuenta en Dientesitos", _utility.mensajeCLaveCreada(paciente));
                paciente.Constrasena = paciente.Constrasena;
                _paciente.addPaciente(paciente);

                return RedirectToAction("VerPacientes");
            }
            ViewData["Mensaje"] = "dni ya registrado";
            return RedirectToAction("RegistrarPaciente");
        }
        public IActionResult VerCitas()
        {
            ViewBag.CurrentPage = "VerCitas";
            var doc             = _doctor.getDoctor(HttpContext);

            return View(_cita.getCitasXDoctor(doc.Iddoctor));
        }
        public IActionResult InfoCita(int IDCita)
        {
            return View(_cita.getCitaDentalXId(IDCita));
        }
        public IActionResult FinalizarCita(int IDCita)
        {
            var cita    = _cita.getCitaDentalXId(IDCita);

            cita.Estado = 2;
            _cita.editCita(cita);

            return RedirectToAction("VerCitas");
        }
        public IActionResult ReservarCita()
        {
            var doc = _doctor.getDoctor(HttpContext);

            return View(_doctor.getDatosCita(doc.Iddoctor));
        }
        [HttpPost]
        public IActionResult ReservarCita(int idTratamiento, int idPaciente, int idSede, DateTime fecha, TimeSpan hora)
        {
            var doc     = _doctor.getDoctor(HttpContext);
            var cita    = _cita.createCitaDental(idSede, idTratamiento, doc.Iddoctor, idPaciente, fecha.ToString(), hora.ToString());

            _cita.registrarCita(cita);

            return RedirectToAction("VerCitas");
        }
        public IActionResult EditarPerfil()
        {
            ViewBag.CurrentPage = "EditarPerfil";
            var doc = _doctor.getDoctor(HttpContext);

            return View(doc);
        }
        [HttpPost]
        public IActionResult EditarPerfil(Doctor docEdit)
        {
            ViewBag.CurrentPage     = "EditarPerfil";
            var doc                 = _doctor.getDoctor(HttpContext);

            doc.Nombre              = docEdit.Nombre;
            doc.ApellidoPaterno     = docEdit.ApellidoPaterno;
            doc.ApellidoMaterno     = docEdit.ApellidoPaterno;
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
            var doc = _doctor.getDoctor(HttpContext);

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
            doc.Constrasena = _utility.convertirSha256(contrasena);
            _doctor.editDoctor(doc);

            return RedirectToAction("EditarPerfil", "Doctor");
        }
        public IActionResult GenerarReporte()
        {
            ViewBag.CurrentPage = "GenerarReporte";
            var doc = _doctor.getDoctor(HttpContext);

            return View(_reporte.getReporte(doc.Iddoctor));
        }
    }
}
