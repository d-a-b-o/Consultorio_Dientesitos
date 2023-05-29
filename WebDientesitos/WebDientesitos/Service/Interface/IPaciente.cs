using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IPaciente
    {
        Paciente    getPaciente(HttpContext httpContext);
        void        editPaciente(Paciente pacienteEdit);
        CitaDental getCitaDental(int IDCita);
        void editCita(CitaDental citaEdit);
        IEnumerable<DatosVerCitas> getCitas(int idPaciente);
        IEnumerable<DatosVerCitas> getCitasFin(int idPaciente);
        DatosCita   getDatosCita();
        DatosCita getDatosCita(int idCita);
        bool CompararFechas(DateTime fechaIngresada);
        void        addCita(int sedeCod, int tratamientoCod, int doctorCod, int pacienteCod, String fecha, String hora);
        String      convertirSha256(String input);
    }
}
