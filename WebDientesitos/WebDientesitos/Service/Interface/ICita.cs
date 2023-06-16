using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface ICita
    {
        void registrarCita(CitaDental cita);
        CitaDental prepareCitaSinUser(CitaSinUser citaSinUser);
        Paciente preparePacienteTemp(CitaSinUser citaSinUser);
        int getIdCorrelativo();
        DatosCita GetDatosCita();

    }
}
