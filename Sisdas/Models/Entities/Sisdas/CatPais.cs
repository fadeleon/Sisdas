using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Catalogo de los paises del mundo
/// </summary>
public partial class CatPais
{
    /// <summary>
    /// Identificador del pais
    /// </summary>
    public int IdPais { get; set; }

    /// <summary>
    /// Nombre del pais
    /// </summary>
    public string NombrePais { get; set; } = null!;
}
