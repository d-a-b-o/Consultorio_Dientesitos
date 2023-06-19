using Microsoft.EntityFrameworkCore;
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
                documento = claimPaciente.Claims
                            .Where(c => c.Type == ClaimTypes.NameIdentifier)
                            .Select(c => c.Value)
                            .SingleOrDefault();
            }
            return conexion.Pacientes
                    .Where(paciente => paciente.Documento == documento)
                    .Single();
        }

        public void addPaciente(Paciente paciente)
        {
            conexion.Pacientes.Add(paciente);
            conexion.SaveChanges();
        }

        public IEnumerable<Paciente> getAllPacientes()
        {
            return conexion.Pacientes;
        }

        public Boolean datosPacienteExisten(Paciente _paciente)
        {
            var lstPacientes = getAllPacientes();

            foreach(Paciente paciente in lstPacientes)
            {
                if(paciente.Documento == _paciente.Documento) { return true; }
            }

            return false;
        }

        public Paciente getPacienteXId(int idPaciente)
        {
            return conexion.Pacientes
                    .Include(c => c.CitaDentals)
                    .ThenInclude(c => c.IdtratamientoNavigation)
                    .Include(c => c.CitaDentals)
                    .ThenInclude(c => c.IdsedeNavigation)
                    .Include(c => c.CitaDentals)
                    .ThenInclude(c => c.IddoctorNavigation)
                    .Where(c => c.Idpaciente == idPaciente)
                    .Single();
        }

        public void editPaciente(Paciente pacienteEdit)
        {
            conexion.Update(pacienteEdit);
            conexion.SaveChanges();
        }

        public bool CompararFechas(DateTime fechaIngresada)
        {
            DateTime fechaActual    = DateTime.Now;
            TimeSpan diferencia     = fechaIngresada - fechaActual;
            int diferenciaEnDias    = Math.Abs(diferencia.Days);

            return diferenciaEnDias <= 7;
        }

        public String convertirSha256(String input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes   = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes    = sha256.ComputeHash(inputBytes);

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
