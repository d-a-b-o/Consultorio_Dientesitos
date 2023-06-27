using Microsoft.EntityFrameworkCore;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class CitaRepository : ICita
    {
        private DientesitosC conexion = new DientesitosC();
        public DatosCita getDatosCita()
        {
            var datos           = new DatosCita();

            datos.Doctors       = conexion.Doctors;
            datos.Sedes         = conexion.Sedes;
            datos.Tratamientos  = conexion.Tratamientos;

            return datos;
        }

        public DatosCita getDatosCita(int idCita)
        {
            var datos           = new DatosCita();

            datos.Doctors       = conexion.Doctors;
            datos.Sedes         = conexion.Sedes;
            datos.Tratamientos  = conexion.Tratamientos;
            datos.citaDental    = getCitaDentalXId(idCita);
            datos.fecha         = DateOnly.Parse(datos.citaDental.Fecha.ToString().Substring(0, 10));

            return datos;
        }

        public CitaDental createCitaDental(int sedeCod, int idTratamiento, int idDoctor, int idPaciente, String fecha, String hora)
        {
            var cita            = new CitaDental();
            var tratamiento     = getTratamientoXId(idTratamiento);

            cita.Idtratamiento  = idTratamiento;
            cita.Iddoctor       = idDoctor;
            cita.Idpaciente     = idPaciente;
            cita.Idsede         = sedeCod;
            cita.Fecha          = DateTime.Parse(fecha);
            cita.Hora           = TimeSpan.Parse(hora);
            cita.Duracion       = tratamiento.Duracion;
            cita.ImportePagar   = tratamiento.Precio;
            cita.Estado         = 0;

            return cita;
        }

        public CitaDental prepareCitaSinUser(CitaSinUser citaSinUser)
        {
            var pacienteTemp        = preparePacienteTemp(citaSinUser);
            var citaDental          = new CitaDental();
            var tratamiento         = getTratamientoXId(citaSinUser.IdTratamiento);

            citaDental.Hora         = citaSinUser.Hora;
            citaDental.Fecha        = citaSinUser.Fecha;
            citaDental.Idpaciente   = pacienteTemp.Idpaciente;
            citaDental.Iddoctor     = citaSinUser.IdDoctor;
            citaDental.Idsede       = citaSinUser.IdSede;
            citaDental.Idtratamiento = citaSinUser.IdTratamiento;
            citaDental.Duracion     = tratamiento.Duracion;
            citaDental.ImportePagar = tratamiento.Precio;
            citaDental.Estado       = 0;

            return citaDental;
        }

        public Paciente preparePacienteTemp(CitaSinUser citaSinUser)
        {
            Paciente pacienteTemp           = new Paciente();

            pacienteTemp.Documento          = citaSinUser.DniPaciente;
            pacienteTemp.Nombre             = citaSinUser.NombrePaciente;
            pacienteTemp.ApellidoPaterno    = citaSinUser.ApellidoPaternoPaciente;
            pacienteTemp.ApellidoMaterno    = citaSinUser.ApellidoPaternoPaciente;
            pacienteTemp.Direccion          = citaSinUser.CorreoPaciente;
            pacienteTemp.Telefono           = citaSinUser.CelularPaciente;
            pacienteTemp.Estado             = 0;
            conexion.Pacientes.Add(pacienteTemp);
            conexion.SaveChanges();

            return pacienteTemp;
        }

        public CitaDental getCitaDentalXId(int idCita)
        {
            return conexion.CitaDentals
                    .Include(c => c.IdtratamientoNavigation)
                    .Include(c => c.IdsedeNavigation)
                    .Include(c => c.IdpacienteNavigation)
                    .Include(c => c.IddoctorNavigation)
                    .Where(c => c.Idcita == idCita)
                    .Single();
        }

        public void registrarCita(CitaDental cita)
        {
            conexion.CitaDentals.Add(cita);
            conexion.SaveChanges();
        }

        public void editCita(CitaDental citaEdit)
        {
            conexion.Update(citaEdit);
            conexion.SaveChanges();
        }

        public IEnumerable<CitaDental> getCitasXPaciente(int idPaciente)
        {
            return conexion.CitaDentals
                    .Include(c => c.IdtratamientoNavigation)
                    .Include(c => c.IdsedeNavigation)
                    .Include(c => c.IdpacienteNavigation)
                    .Include(c => c.IddoctorNavigation)
                    .Where(cita => cita.Idpaciente == idPaciente)
                    .ToList();
        }
        public IEnumerable<CitaDental> getCitasPendientesXPaciente(int idPaciente)
        {
            return getCitasXPaciente(idPaciente)
                    .Where(c => c.Estado == 0 || c.Estado == 1)
                    .ToList();
        }
        public IEnumerable<CitaDental> getCitasFinalizadasXPaciente(int idPaciente)
        {
            return getCitasXPaciente(idPaciente)
                    .Where(c => c.Estado == 2 || c.Estado == 3)
                    .ToList();
        }
        public IEnumerable<CitaDental> getCitasXDoctor(int idDoctor)
        {
            return conexion.CitaDentals
                    .Include(c => c.IdtratamientoNavigation)
                    .Include(c => c.IdsedeNavigation)
                    .Include(c => c.IdpacienteNavigation)
                    .Include(c => c.IddoctorNavigation)
                    .Where(c => c.Iddoctor == idDoctor)
                    .Where(c => c.Estado != 2 || c.Estado != 3)
                    .ToList();
        }

        public Tratamiento getTratamientoXId(int idTratamiento)
        {
            return conexion.Tratamientos
                    .Where(tratamiento => tratamiento.Idtratamiento == idTratamiento)
                    .Single();
        }
    }
}
