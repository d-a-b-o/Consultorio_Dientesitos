namespace WebDientesitos.Models
{
    public class DatosCita
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public IEnumerable<Tratamiento> Tratamientos { get; set; }
        public IEnumerable<Sede> Sedes { get; set; }
        public CitaDental citaDental { get; set; }
        public DateOnly fecha {  get; set; }
        public CitaSinUser citaSinUser { get; set; }
    }
}
