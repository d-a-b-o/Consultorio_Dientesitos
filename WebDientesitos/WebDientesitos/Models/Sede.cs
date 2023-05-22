using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Sede
{
    public int Idsede { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Gerente { get; set; }

    public virtual ICollection<CitaDental> CitaDentals { get; set; } = new List<CitaDental>();
}
