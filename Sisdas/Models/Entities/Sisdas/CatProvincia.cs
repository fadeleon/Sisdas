using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla con los datos de las provincias de Panamá
/// </summary>
public partial class CatProvincia
{
    public int IdProvincia { get; set; }

    /// <summary>
    /// Nombre de las provincias de Panamá
    /// </summary>
    public string NombreProvincia { get; set; } = null!;

    /// <summary>
    /// Cóidgo de referencia con catálogo de provincias del MINSA
    /// </summary>
    public string? CodRefMinsa { get; set; }

    public int Estatus { get; set; }
}
