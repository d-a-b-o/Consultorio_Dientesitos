using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Paciente
{
    public int Idpaciente { get; set; }

    public string? Documento { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Direccion { get; set; }

    public string? Constrasena { get; set; }

    public string? Telefono { get; set; }

    public string? Edad { get; set; }

    public byte? Estado { get; set; }

    public virtual ICollection<CitaDental> CitaDentals { get; set; } = new List<CitaDental>();

    public virtual ICollection<HistorialMedico> HistorialMedicos { get; set; } = new List<HistorialMedico>();
}
