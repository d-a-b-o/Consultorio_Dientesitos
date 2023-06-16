namespace WebDientesitos.Models
{
    public class DatosCitaDoctor
    {
        public IEnumerable<Tratamiento> Tratamientos { get; set; }
        public IEnumerable<Sede> Sedes { get; set; }
        public IEnumerable<Paciente> Pacientes { get; set; }
    }
}
