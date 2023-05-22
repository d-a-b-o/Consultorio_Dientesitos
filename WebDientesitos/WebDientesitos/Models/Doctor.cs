using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Doctor
{
    public int Iddoctor { get; set; }

    public string? Dni { get; set; }

    public string? Especialidad { get; set; }

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? NumeroColegioMedico { get; set; }

    public string? Constrasena { get; set; }

    public virtual ICollection<CitaDental> CitaDentals { get; set; } = new List<CitaDental>();
}
