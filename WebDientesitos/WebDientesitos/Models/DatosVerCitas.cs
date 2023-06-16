namespace WebDientesitos.Models
{
    public class DatosVerCitas
    {
        public int IDCita { get; set; }
        public Tratamiento Tratamiento { get; set; }
        public Doctor Doctor { get; set; }
        public Sede Sede { get; set; }
        public String Fecha { get; set; }
        public String Hora { get; set; }
        public String Duracion { get; set; }
        public String Importe { get; set; }
        public String Estado { get; set; }
    }
}
