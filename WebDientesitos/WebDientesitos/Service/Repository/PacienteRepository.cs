using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class PacienteRepository : IPaciente
    {
        private DientesitosC conexion = new DientesitosC();
        public Paciente getPaciente(HttpContext httpContext)
        {
            ClaimsPrincipal claimPaciente = httpContext.User;
            String documento = "";
            if (claimPaciente.Identity.IsAuthenticated)
            {
                documento = claimPaciente.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value).SingleOrDefault();
            }
            return (from Paciente in conexion.Pacientes
                    where Paciente.Documento == documento
                    select Paciente).Single();
        }
        public void editPaciente(Paciente pacienteEdit)
        {
            conexion.Update(pacienteEdit);
            conexion.SaveChanges();
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
