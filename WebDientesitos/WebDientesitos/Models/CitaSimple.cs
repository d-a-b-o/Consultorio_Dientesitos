namespace WebDientesitos.Models
{
    public class CitaSimple
    {
        public Tratamiento Tratamiento { get; set; }

        public Doctor Doctor { get; set; }

        public Paciente Paciente { get; set; }

        public Sede Sede { get; set; }

        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public int Duracion { get; set; }

        public decimal ImportePagar { get; set; }

        public byte Estado { get; set; }
    }
}
