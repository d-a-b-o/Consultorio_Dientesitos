using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface ICita
    {
        void registrarCita(CitaDental cita);
        CitaDental prepareCitaSinUser(CitaSinUser citaSinUser);
        Paciente preparePacienteTemp(CitaSinUser citaSinUser);
        DatosCita getDatosCita();
        DatosCita getDatosCita(int idCita);
        CitaDental createCitaDental(int sedeCod, int idTratamiento, int idDoctor, int idPaciente, String fecha, String hora);
        CitaDental getCitaDentalXId(int iDCita);
        void editCita(CitaDental citaEdit);
        IEnumerable<CitaDental> getCitasXPaciente(int idPaciente);
        IEnumerable<CitaDental> getCitasXDoctor(int idDoctor);
        IEnumerable<DatosVerCitas> getDatosVerCitaXPaciente(int idPaciente, Boolean finalizadas);
        DatosVerCitas createDatosVerCitas(CitaDental cita);
        Tratamiento getTratamientoXId(int idTratamiento);
        Doctor getDoctorXId(int idDoctor);
        Sede getSedeXId(int idSede);
        String getEstadoCita(int estado);
        Boolean isCitaValid(string estado, bool finalizadas);
    }
}
