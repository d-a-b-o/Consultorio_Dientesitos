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
            return conexion.Doctors
                    .Where(doctor => doctor.Dni == dni)
                    .Where(doctor => doctor.Constrasena == Contrasena)
                    .SingleOrDefault();
        }
        public Paciente validarPaciente(String dni, String Contrasena)
        {
            return conexion.Pacientes
                    .Where(paciente => paciente.Documento == dni)
                    .Where(paciente => paciente.Constrasena == Contrasena)
                    .SingleOrDefault();
        }
    }
}
