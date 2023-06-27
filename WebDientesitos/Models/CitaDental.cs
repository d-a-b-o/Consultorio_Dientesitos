using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class CitaDental
{
    public int Idcita { get; set; }

    public int? Idtratamiento { get; set; }

    public int? Iddoctor { get; set; }

    public int? Idpaciente { get; set; }

    public int? Idsede { get; set; }

    public DateTime? Fecha { get; set; }

    public TimeSpan? Hora { get; set; }

    public int? Duracion { get; set; }

    public decimal? ImportePagar { get; set; }

    public byte? Estado { get; set; }

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Doctor? IddoctorNavigation { get; set; }

    public virtual Paciente? IdpacienteNavigation { get; set; }

    public virtual Sede? IdsedeNavigation { get; set; }

    public virtual Tratamiento? IdtratamientoNavigation { get; set; }
}
