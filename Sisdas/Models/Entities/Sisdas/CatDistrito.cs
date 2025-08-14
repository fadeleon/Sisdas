using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla con los datos de los distritos de Panamá
/// </summary>
public partial class CatDistrito
{
    public int IdDistrito { get; set; }

    /// <summary>
    /// Nombre del distrito de Panamá
    /// </summary>
    public string NombreDistrito { get; set; } = null!;

    /// <summary>
    /// Provincia a la que pertenece el distrito
    /// </summary>
    public int IdProvincia { get; set; }

    /// <summary>
    /// Código de referencia con los catálogos del MINSA
    /// </summary>
    public string? CodRefMinsa { get; set; }

    public int IdRegion { get; set; }
}
