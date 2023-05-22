using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebDientesitos.Models;

namespace WebDientesitos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}