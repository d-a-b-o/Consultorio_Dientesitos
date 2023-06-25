using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IPaciente
    {
        Paciente    getPaciente(HttpContext httpContext);
        void        addPaciente(Paciente paciente);
        Paciente    getPacienteXId(int idPaciente);
        IEnumerable<Paciente> getPacientesXDoctor(int idDoctor);
        Boolean     hasCitaWithDoctor(int idPaciente, int idDoctor);
        void        editPaciente(Paciente pacienteEdit);
        Boolean     datosPacienteExisten(Paciente paciente);
        bool        CompararFechas(DateTime fechaIngresada);
    }
}
