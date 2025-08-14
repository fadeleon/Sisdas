using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla con los datos de las regiones de salud
/// </summary>
public partial class CatRegionSalud
{
    /// <summary>
    /// Código correlativo númerico y autoincremental de las regiones de salud del MINSA
    /// </summary>
    public int IdRegion { get; set; }

    /// <summary>
    /// Nombre de la Región de Salud
    /// </summary>
    public string NombreRegion { get; set; } = null!;

    /// <summary>
    /// Identificador de la provincia
    /// </summary>
    public int? IdProvincia { get; set; }
}
