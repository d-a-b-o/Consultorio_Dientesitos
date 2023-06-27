using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class HistorialMedico
{
    public int Idhistorial { get; set; }

    public int? Idpaciente { get; set; }

    public string? Alergias { get; set; }

    public string? Detalles { get; set; }

    public string? Resultado { get; set; }

    public DateTime? Fecha { get; set; }

    public virtual Paciente? IdpacienteNavigation { get; set; }
}
