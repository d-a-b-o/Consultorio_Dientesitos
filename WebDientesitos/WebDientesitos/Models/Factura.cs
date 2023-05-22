using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Factura
{
    public int Idfactura { get; set; }

    public int? Idcita { get; set; }

    public string? MetodoPago { get; set; }

    public decimal? Igv { get; set; }

    public decimal? TotalPagar { get; set; }

    public virtual CitaDental? IdcitaNavigation { get; set; }
}
