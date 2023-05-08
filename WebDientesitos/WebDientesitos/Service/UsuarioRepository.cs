using WebDientesitos.Models;

namespace WebDientesitos.Service
{
    public class UsuarioRepository : IUsuario
    {
        private List<Usuario> lst = new List<Usuario>();
        public void Add(Usuario usuario)
        {
            lst.Add(usuario);
        }

        public Usuario getUser(double id)
        {
            for(int i = 0; i < lst.Count; i++)
            {
                if (lst[i].dni.Equals(id))
                {
                    return lst[i];
                }
            }
            return null;
        }

        public int getSize()
        {
            return lst.Count;
        }

        public void primerAdmin()
        {
            Usuario temp = new Usuario();
            temp.nombre = "Admin";
            temp.apellidos = "Admin";
            temp.correo = "admin@gmail.com";
            temp.celular = 123456789;
            temp.dni = 12345678;
            temp.esAdmin = true;
            temp.contrasena = "12345";
            Add(temp);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return lst;
        }
    }
}
