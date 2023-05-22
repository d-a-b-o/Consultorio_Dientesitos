using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IUsuario
    {
        Doctor validarDoctor(String dni, String Contrasena);
        Paciente validarPaciente(String dni, String Contrasena);
        String convertirSha256(String input);
    }
}
