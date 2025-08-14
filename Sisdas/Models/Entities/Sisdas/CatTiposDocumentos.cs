using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

public partial class CatTiposDocumentos
{
    public int IdTipoDocumento { get; set; }

    public string? NombreDocumento { get; set; }

    public int? Estado { get; set; }
}
