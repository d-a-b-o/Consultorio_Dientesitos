using Microsoft.AspNetCore.Mvc;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Controllers
{
    public class CitaController : Controller
    {
        private readonly ICita _cita;

        public CitaController(ICita cita)
        {
            _cita = cita;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
