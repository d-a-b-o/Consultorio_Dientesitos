using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IDoctor
    {
        Doctor getDoctor(HttpContext httpContext);
        void editDoctor(Doctor docEdit);
        void editCita(CitaDental cita);
        CitaDental getCita(int IDCita);
        List<CitaDental> getCitasP(int IdPaciente);
        IEnumerable<Paciente> getAllPacientes(int IdDoctor);
        IEnumerable<CitaDental> getCitas(int IdDoctor);
        CitaSimple getCitasPaciente(int IdPaciente);
        Paciente getPaciente(int IDPaciente);
        void addPaciente(Paciente paciente);
        String generarClaveTemp();
        String convertirSha256(String input);
        void EnviarCorreo(String destinatario, String asunto, String cuerpo);
        String mensajeClave(Paciente paciente);
    }
}
