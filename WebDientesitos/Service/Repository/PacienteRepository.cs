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
        private CitaRepository _cita = new CitaRepository();

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

        public IEnumerable<Paciente> getPacientesXDoctor(int idDoctor)
        {
            var lstPacientes = new List<Paciente>();
            var pacientes = getAllPacientes();

            foreach (Paciente paciente in pacientes)
            {
                if (hasCitaWithDoctor(paciente.Idpaciente, idDoctor))
                {
                    lstPacientes.Add(paciente);
                }
            }

            return lstPacientes;
        }

        public Boolean hasCitaWithDoctor(int idPaciente, int idDoctor)
        {
            var lstCitas = _cita.getCitasXPaciente(idPaciente);

            foreach (CitaDental cita in lstCitas)
            {
                if (cita.Iddoctor == idDoctor)
                {
                    return true;
                }
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
    }
}
