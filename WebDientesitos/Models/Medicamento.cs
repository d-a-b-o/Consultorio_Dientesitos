using System;
using System.Collections.Generic;

namespace WebDientesitos.Models;

public partial class Medicamento
{
    public int Idmedicamento { get; set; }

    public string? Descripcion { get; set; }

    public string? UnidadMedida { get; set; }

    public string? Nombre { get; set; }
}
