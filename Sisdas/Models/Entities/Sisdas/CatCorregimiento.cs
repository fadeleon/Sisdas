using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla con los datos de los corregimientos de Panamá
/// </summary>
public partial class CatCorregimiento
{
    public int IdCorregimiento { get; set; }

    /// <summary>
    /// Nombre del corregimiento de Panamá
    /// </summary>
    public string NombreCorregimiento { get; set; } = null!;

    /// <summary>
    /// Código del distrito al que pertenece el corregimiento.
    /// </summary>
    public int IdDistrito { get; set; }

    /// <summary>
    /// Código de referencia con los catálogos del MINSA.
    /// </summary>
    public string? CodRefMinsa { get; set; }
}
