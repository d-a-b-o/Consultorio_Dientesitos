using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IPaciente
    {
        Paciente    getPaciente(HttpContext httpContext);
        void        addPaciente(Paciente paciente);
        Paciente    getPacienteXId(int idPaciente);
        void        editPaciente(Paciente pacienteEdit);
        Boolean datosPacienteExisten(Paciente paciente);
        bool        CompararFechas(DateTime fechaIngresada);
        String      convertirSha256(String input);
    }
}
