using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IUtility
    {
        String convertirSha256(String input);
        String generarClaveTemporal();
        void enviarCorreo(String destinatario, String asunto, String cuerpo);
        String mensajeCLaveCreada(Paciente paciente);
        String mensajeCitaReservada(Paciente paciente);
    }
}
