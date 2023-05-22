using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IPaciente
    {
        Paciente getPaciente(HttpContext httpContext);
        void editPaciente(Paciente pacienteEdit);
        String convertirSha256(String input);
    }
}
