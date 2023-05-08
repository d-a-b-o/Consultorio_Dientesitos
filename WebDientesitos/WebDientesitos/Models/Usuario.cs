namespace WebDientesitos.Models
{
    public class Usuario
    {
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string correo { get; set; }
        public double celular { get; set; }
        public string contrasena { get; set; }
        public double dni { get; set; }
        public bool esAdmin { get; set; }
    }
}
