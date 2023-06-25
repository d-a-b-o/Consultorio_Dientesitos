using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface ICita
    {
        CitaDental  getCitaDentalXId(int iDCita);
        void        registrarCita(CitaDental cita);
        void        editCita(CitaDental citaEdit);
        CitaDental  prepareCitaSinUser(CitaSinUser citaSinUser);
        CitaDental  createCitaDental(int sedeCod, int idTratamiento, int idDoctor, int idPaciente, String fecha, String hora);
        Paciente    preparePacienteTemp(CitaSinUser citaSinUser);
        DatosCita   getDatosCita();
        DatosCita   getDatosCita(int idCita);
        Tratamiento getTratamientoXId(int idTratamiento);
        IEnumerable<CitaDental> getCitasXPaciente(int idPaciente);
        IEnumerable<CitaDental> getCitasPendientesXPaciente(int idPaciente);
        IEnumerable<CitaDental> getCitasFinalizadasXPaciente(int idPaciente);
        IEnumerable<CitaDental> getCitasXDoctor(int idDoctor);
    }
}
