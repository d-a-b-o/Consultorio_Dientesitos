using Microsoft.AspNetCore.Mvc;
using WebDientesitos.Models;
using WebDientesitos.Service;

namespace WebDientesitos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuario _iusuario;
        public UsuarioController(IUsuario usuario)
        {
            _iusuario = usuario;
            if (_iusuario.getSize() == 0)
            {
                _iusuario.primerAdmin();
            }
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Listado(Usuario usuario)
        {
            _iusuario.Add(usuario);
            return View(usuario);
        }
        public IActionResult MenuUsuario(Usuario usuario)
        {
            Usuario user = _iusuario.getUser(usuario.dni);
            return View(user);
        }
    }
}
