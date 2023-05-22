using System.Security.Cryptography;
using System.Text;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class UsuarioRepository : IUsuario
    {
        private DientesitosC conexion = new DientesitosC();
        public Doctor validarDoctor(String dni, String Contrasena)
        {
            var docEncontrado = (from Doctor in conexion.Doctors
                                 where Doctor.Dni == dni && Doctor.Constrasena == Contrasena
                                 select Doctor).SingleOrDefault();
            return docEncontrado;
        }
        public Paciente validarPaciente(String dni, String Contrasena)
        {
            var pacienteEncontrado = (from Paciente in conexion.Pacientes
                                      where Paciente.Documento == dni && Paciente.Constrasena == Contrasena
                                      select Paciente).SingleOrDefault();
            return pacienteEncontrado;
        }
        public String convertirSha256(String input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
