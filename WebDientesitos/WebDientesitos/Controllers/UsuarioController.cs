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
        public IActionResult IniciarSesion(String rol, String dni, String contrasena)
        {
            if(contrasena.Length == 7)
            {
                Paciente pacienteTemp = _usuario.validarPaciente(dni, contrasena);
                if (pacienteTemp != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, dni)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties);
                    return RedirectToAction("EditarContrasena", "Paciente");
                }
            }
            contrasena = _usuario.convertirSha256(contrasena);
            if (rol.Equals("doctor"))
            {
                Doctor docTemp = _usuario.validarDoctor(dni, contrasena);
                if(docTemp != null)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, dni)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties);
                    return RedirectToAction("MenuDoctor", "Doctor");
                }
            }
            else
            {
                Paciente pacienteTemp = _usuario.validarPaciente(dni, contrasena);
                if(pacienteTemp != null && pacienteTemp.Estado == 1)
                {
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.NameIdentifier, dni)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    AuthenticationProperties properties = new AuthenticationProperties()
                    {
                        AllowRefresh = true
                    };
                    HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        properties);
                    return RedirectToAction("MenuPaciente", "Paciente");
                }
            }
            ViewData["Mensaje"] = "No se encontraron coincidencias";
            return View();
        }
        public IActionResult CerrarSesion()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Inicio", "Home");
        }
    }
}
