using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IDoctor
    {
        Doctor getDoctor(HttpContext httpContext);
        void editDoctor(Doctor docEdit);
        IEnumerable<Paciente> getPacientesXDoctor(int idDoctor);
        Boolean hasCitaWithDoctor(int idPaciente, int idDoctor);
        List<CitaDental> getCitasXPaciente(int IdPaciente);
        IEnumerable<Paciente> getAllPacientes();
        DatosCitaDoctor getDatosCita(int idDoctor);
        String generarClaveTemp();
        String convertirSha256(String input);
        void EnviarCorreo(String destinatario, String asunto, String cuerpo);
        String mensajeClave(Paciente paciente);
    }
}
