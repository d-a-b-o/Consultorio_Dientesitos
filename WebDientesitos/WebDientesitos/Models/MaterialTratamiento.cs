using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class MaterialTratamiento
{
    public int? Idmaterial { get; set; }

    public int? Idtratamiento { get; set; }

    public int? Cantidad { get; set; }

    public virtual MaterialMedico? IdmaterialNavigation { get; set; }

    public virtual Tratamiento? IdtratamientoNavigation { get; set; }
}
