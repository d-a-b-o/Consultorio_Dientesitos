using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IDoctor
    {
        Doctor getDoctor(HttpContext httpContext);
        void editDoctor(Doctor docEdit);
        IEnumerable<Paciente> getAllPacientes();
        void addPaciente(Paciente paciente);
        String generarClaveTemp();
        String convertirSha256(String input);
        void EnviarCorreo(String destinatario, String asunto, String cuerpo);
        String mensajeClave(Paciente paciente);
    }
}
