namespace WebDientesitos.Models
{
    public class CitaSinUser
    {
        public string DniPaciente { get; set; }
        public string NombrePaciente { get; set; } 
        public string ApellidoPaternoPaciente { get; set; }
        public string ApellidoMaternoPaciente { get; set; }
        public string CorreoPaciente { get; set; }
        public string CelularPaciente { get; set; }
        public int IdSede { get; set; }
        public int IdTratamiento { get; set; }
        public int IdDoctor { get; set; }
        public DateTime Fecha { get; set; }

        public TimeSpan Hora { get; set; }

        public int? Duracion { get; set; }

        public decimal? ImportePagar { get; set; }

        public byte? Estado { get; set; }
    }
}
