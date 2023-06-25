using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class DoctorRepository : IDoctor
    {
        private DientesitosC conexion           = new DientesitosC();
        private PacienteRepository _paciente    = new PacienteRepository();

        public Doctor getDoctor(HttpContext httpContext)
        {
            ClaimsPrincipal claimDoctor = httpContext.User;
            String dni = "";
            if (claimDoctor.Identity.IsAuthenticated)
            {
                dni = claimDoctor.Claims
                    .Where(c => c.Type == ClaimTypes.NameIdentifier)
                    .Select(c => c.Value)
                    .SingleOrDefault();
            }

            return conexion.Doctors
                    .Where(doctor => doctor.Dni == dni)
                    .Single();
        }

        public void editDoctor(Doctor docEdit)
        {
            conexion.Update(docEdit);
            conexion.SaveChanges();
        }

        public DatosCitaDoctor getDatosCita(int idDoctor)
        {
            var data            = new DatosCitaDoctor();
            var lstPaciente     = _paciente.getPacientesXDoctor(idDoctor);

            data.Tratamientos   = conexion.Tratamientos;
            data.Pacientes      = lstPaciente.Where(c => c.Estado == 1).ToList();
            data.Sedes          = conexion.Sedes;

            return data;
        }
    }
}
