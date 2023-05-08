using WebDientesitos.Models;

namespace WebDientesitos.Service
{
    public interface IUsuario
    {
        void Add(Usuario usuario);
        IEnumerable<Usuario> GetAll();
        public Usuario getUser(double id);
        public void primerAdmin();
        public int getSize();
    }
}
