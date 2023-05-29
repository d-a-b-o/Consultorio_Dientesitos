namespace WebDientesitos.Models
{
    public class InfoPacientes
    {
        public Paciente paciente { get; set; }
        public IEnumerable<CitaSimple> citas { get; set; }
    }
}
