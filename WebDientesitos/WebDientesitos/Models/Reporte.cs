namespace WebDientesitos.Models
{
    public class Reporte
    {
        public IEnumerable<Paciente> Pacientes { get; set;}
        public IEnumerable<CitaDental> CitasDentales { get; set;}
        public IEnumerable<Tratamiento> Tratamientos { get; set;}
    }
}
