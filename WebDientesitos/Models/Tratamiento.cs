using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Tratamiento
{
    public int Idtratamiento { get; set; }

    public string? Nombre { get; set; }

    public string? Descripcion { get; set; }

    public int? Duracion { get; set; }

    public decimal? Precio { get; set; }

    public virtual ICollection<CitaDental> CitaDentals { get; set; } = new List<CitaDental>();
}
