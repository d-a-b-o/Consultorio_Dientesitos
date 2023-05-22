using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Recetum
{
    public int? Idcita { get; set; }

    public int? Idmedicamento { get; set; }

    public int? Cantidad { get; set; }

    public string? Dosis { get; set; }

    public virtual CitaDental? IdcitaNavigation { get; set; }

    public virtual Medicamento? IdmedicamentoNavigation { get; set; }
}
