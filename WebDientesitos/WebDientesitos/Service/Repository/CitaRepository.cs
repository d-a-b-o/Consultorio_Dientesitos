using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class CitaRepository : ICita
    {
        private DientesitosC conexion = new DientesitosC();
        public DatosCita GetDatosCita()
        {
            DatosCita datos = new DatosCita();
            datos.Doctors = conexion.Doctors;
            datos.Sedes = conexion.Sedes;
            datos.Tratamientos = conexion.Tratamientos;
            return datos;
        }

        public int getIdCorrelativo()
        {
            int maxId = conexion.Pacientes.Max(p => p.Idpaciente);
            return 19;
        }

        public CitaDental prepareCitaSinUser(CitaSinUser citaSinUser)
        {
            Paciente pacienteTemp = preparePacienteTemp(citaSinUser);
            CitaDental citaDental = new CitaDental();
            citaDental.Hora = citaSinUser.Hora;
            citaDental.Fecha = citaSinUser.Fecha;
            citaDental.Idpaciente = pacienteTemp.Idpaciente;
            citaDental.Iddoctor = citaSinUser.IdDoctor;
            citaDental.Idsede = citaSinUser.IdSede;
            citaDental.Idtratamiento = citaSinUser.IdTratamiento;
            citaDental.Duracion = 60;
            citaDental.ImportePagar = 300;
            citaDental.Estado = 0;

            return citaDental;
        }

        public Paciente preparePacienteTemp(CitaSinUser citaSinUser)
        {
            Paciente pacienteTemp = new Paciente();

            pacienteTemp.Documento = citaSinUser.DniPaciente;
            pacienteTemp.Nombre = citaSinUser.NombrePaciente;
            pacienteTemp.ApellidoPaterno = citaSinUser.ApellidoPaternoPaciente;
            pacienteTemp.ApellidoMaterno = citaSinUser.ApellidoPaternoPaciente;
            pacienteTemp.Direccion = citaSinUser.CorreoPaciente;
            pacienteTemp.Telefono = citaSinUser.CelularPaciente;
            pacienteTemp.Estado = 0;

            conexion.Pacientes.Add(pacienteTemp);
            conexion.SaveChanges();
            return pacienteTemp;
        }

        public void registrarCita(CitaDental cita)
        {
            conexion.CitaDentals.Add(cita);
            conexion.SaveChanges();
        }
    }
}
