using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class ReporteRepository : IReporte
    {
        private DientesitosC conexion = new DientesitosC();
        private PacienteRepository _paciente = new PacienteRepository();
        private CitaRepository _cita = new CitaRepository();
        public Reporte getReporte(int idDoctor)
        {
            var reporte = new Reporte();

            reporte.Pacientes = _paciente.getPacientesXDoctor(idDoctor);
            reporte.CitasDentales = _cita.getCitasXDoctor(idDoctor);
            reporte.Tratamientos = conexion.Tratamientos.ToList();

            return reporte;
        }
    }
}
