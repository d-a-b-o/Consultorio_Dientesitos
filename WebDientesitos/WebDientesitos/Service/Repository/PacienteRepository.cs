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
        public CitaDental getCitaDental(int IDCita)
        {
            return (from Cita in conexion.CitaDentals
                    where Cita.Idcita == IDCita
                    select Cita).Single();
        }
        public void editCita(CitaDental citaEdit)
        {
            conexion.Update(citaEdit);
            conexion.SaveChanges();
        }
        public IEnumerable<DatosVerCitas> getCitas(int idPaciente)
        {
            IEnumerable<CitaDental> lst = (from Cita in conexion.CitaDentals
                                           where Cita.Idpaciente == idPaciente
                                           select Cita).ToList();
            List<DatosVerCitas> lstVer = new List<DatosVerCitas>();
            foreach (CitaDental cita in lst)
            {
                DatosVerCitas temp = new DatosVerCitas();
                Tratamiento trat = (from tratamiento in conexion.Tratamientos
                                    where tratamiento.Idtratamiento == cita.Idtratamiento
                                    select tratamiento).Single();
                Doctor doc = (from doctor in conexion.Doctors
                              where doctor.Iddoctor == cita.Iddoctor
                              select doctor).Single();
                Sede sed = (from sede in conexion.Sedes
                            where sede.Idsede == cita.Idsede
                            select sede).Single();
                temp.IDCita = cita.Idcita;
                temp.Tratamiento = trat;
                temp.Doctor = doc;
                temp.Sede = sed;
                temp.Fecha = cita.Fecha.ToString();
                temp.Hora = cita.Hora.ToString();
                temp.Duracion = cita.Duracion.ToString();
                temp.Importe = cita.ImportePagar.ToString();
                switch (cita.Estado)
                {
                    case 0:
                        temp.Estado = "Agendado";
                        break;
                    case 1:
                        temp.Estado = "Pagada";
                        break;
                    case 2:
                        temp.Estado = "Finalizado";
                        break;
                    case 3:
                        temp.Estado = "Cancelado";
                        break;
                }
                if(cita.Estado!=2 && cita.Estado!=3)
                {
                    lstVer.Add(temp);
                }
            }
            return lstVer;
        }
        public IEnumerable<DatosVerCitas> getCitasFin(int idPaciente)
        {
            IEnumerable<CitaDental> lst = (from Cita in conexion.CitaDentals
                                           where Cita.Idpaciente == idPaciente
                                           select Cita).ToList();
            List<DatosVerCitas> lstVer = new List<DatosVerCitas>();
            foreach (CitaDental cita in lst)
            {
                DatosVerCitas temp = new DatosVerCitas();
                Tratamiento trat = (from tratamiento in conexion.Tratamientos
                                    where tratamiento.Idtratamiento == cita.Idtratamiento
                                    select tratamiento).Single();
                Doctor doc = (from doctor in conexion.Doctors
                              where doctor.Iddoctor == cita.Iddoctor
                              select doctor).Single();
                Sede sed = (from sede in conexion.Sedes
                            where sede.Idsede == cita.Idsede
                            select sede).Single();
                temp.IDCita = cita.Idcita;
                temp.Tratamiento = trat;
                temp.Doctor = doc;
                temp.Sede = sed;
                temp.Fecha = cita.Fecha.ToString();
                temp.Hora = cita.Hora.ToString();
                temp.Duracion = cita.Duracion.ToString();
                temp.Importe = cita.ImportePagar.ToString();
                switch (cita.Estado)
                {
                    case 0:
                        temp.Estado = "Agendado";
                        break;
                    case 1:
                        temp.Estado = "Pagada";
                        break;
                    case 2:
                        temp.Estado = "Finalizado";
                        break;
                    case 3:
                        temp.Estado = "Cancelado";
                        break;
                }
                if (cita.Estado == 2 || cita.Estado == 3)
                {
                    lstVer.Add(temp);
                }
            }
            return lstVer;
        }
        public DatosCita getDatosCita()
        {
            DatosCita datos     = new DatosCita();
            datos.Doctors       = conexion.Doctors;
            datos.Sedes         = conexion.Sedes;
            datos.Tratamientos  = conexion.Tratamientos;
            return datos;
        }
        public DatosCita getDatosCita(int idCita)
        {
            DatosCita datos = new DatosCita();
            datos.Doctors = conexion.Doctors;
            datos.Sedes = conexion.Sedes;
            datos.Tratamientos = conexion.Tratamientos;
            datos.citaDental = getCitaDental(idCita);
            datos.fecha = DateOnly.Parse(datos.citaDental.Fecha.ToString().Substring(0, 10));
            return datos;
        }
        public bool CompararFechas(DateTime fechaIngresada)
        {
            DateTime fechaActual = DateTime.Now;
            TimeSpan diferencia = fechaIngresada - fechaActual;
            int diferenciaEnDias = Math.Abs(diferencia.Days);

            return diferenciaEnDias <= 7;
        }
        public void addCita(int sedeCod, int tratamientoCod, int doctorCod, int pacienteCod, String fecha, String hora)
        {
            CitaDental cita     = new CitaDental();
            cita.Idtratamiento  = tratamientoCod;
            cita.Iddoctor       = doctorCod;
            cita.Idpaciente     = pacienteCod;
            cita.Idsede         = sedeCod;
            cita.Fecha          = DateTime.Parse(fecha);
            cita.Hora           = TimeSpan.Parse(hora);
            cita.Duracion       = 60;
            cita.ImportePagar   = 300;
            cita.Estado         = 0;
            conexion.CitaDentals.Add(cita);
            conexion.SaveChanges();
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
