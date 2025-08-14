using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla que contiene el nombre del servicio de salud
/// </summary>
public partial class CatServicio
{
    /// <summary>
    /// Identificador del servicio de salud
    /// </summary>
    public int IdServicio { get; set; }

    /// <summary>
    /// Nombre del servicio de salud
    /// </summary>
    public string NombreServicio { get; set; } = null!;
}
