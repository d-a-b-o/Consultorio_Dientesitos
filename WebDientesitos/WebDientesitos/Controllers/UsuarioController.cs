using Microsoft.AspNetCore.Mvc;

using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace WebDientesitos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _usuario;
        public UsuarioController(IUsuario usuario)
        {
            _usuario = usuario;
        }
        public IActionResult IniciarSesion()
        {
            return View();
        }
        [HttpPost]
        public IActionResult IniciarSesion(string rol, string dni, string contrasena)
        {
            if (contrasena.Length == 7)
            {
                var pacienteTemp = _usuario.validarPaciente(dni, contrasena);
                if (pacienteTemp != null)
                {
                    return IniciarSesionExitoso(rol, dni);
                }
            }
            contrasena = _usuario.convertirSha256(contrasena);
            if (rol.Equals("doctor"))
            {
                var docTemp = _usuario.validarDoctor(dni, contrasena);
                if (docTemp != null)
                {
                    return IniciarSesionExitoso(rol, dni);
                }
            }
            else
            {
                var pacienteTemp = _usuario.validarPaciente(dni, contrasena);
                if (pacienteTemp != null && pacienteTemp.Estado == 1)
                {
                    return IniciarSesionExitoso(rol, dni);
                }
            }
            ViewData["Mensaje"] = "No se encontraron coincidencias";
            return View();
        }
        private IActionResult IniciarSesionExitoso(string rol, string dni)
        {
            var claims          = new List<Claim>() { new Claim(ClaimTypes.NameIdentifier, dni) };
            var claimsIdentity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var properties      = new AuthenticationProperties() { AllowRefresh = true };

            HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties);

            if (rol.Equals("doctor"))
            {
                return RedirectToAction("MenuDoctor", "Doctor");
            }
            else
            {
                return RedirectToAction("MenuPaciente", "Paciente");
            }
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Inicio", "Home");
        }
    }
}
