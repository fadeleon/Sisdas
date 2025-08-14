using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla con los datos de la Unidad Notificadora
/// </summary>
public partial class CatUnidadNotificadora
{
    public int IdUn { get; set; }

    /// <summary>
    /// Nombre de la Unidad Notificadora
    /// </summary>
    public string NombreUn { get; set; } = null!;

    /// <summary>
    /// Sector al que pertenece la Unidad notificadora:
    /// 1 = MINSA
    /// 2 = Caja del Seguro Social
    /// 3 = Privado
    /// 4 = ONG
    /// 5 = Cooperación Externa
    /// 6 = Otros
    /// </summary>
    public int SectorUn { get; set; }

    /// <summary>
    /// Código de la región de salud a la que pertenece.
    /// </summary>
    public int IdRegion { get; set; }

    /// <summary>
    /// Código del corregimiento en el que está ubicada la unidad notificadora
    /// </summary>
    public int IdCorregimiento { get; set; }

    /// <summary>
    /// Código referencia del MINSA
    /// </summary>
    public string? CodRefMinsa { get; set; }

    /// <summary>
    /// Estado del registro, 1 = Habilitado y 0 = Deshabilitado
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// Identificador del tipo de instalacion
    /// </summary>
    public int IdtipoInstalacion { get; set; }

    /// <summary>
    /// 1= Es sitio centinela de Influenza
    /// </summary>
    public int Centinela { get; set; }

    /// <summary>
    /// 1= Unidad centinela de Influenza que vigila IRAG
    /// </summary>
    public int Irag { get; set; }

    /// <summary>
    /// 1= Unidad centinela de Influenza que vigila ETI
    /// </summary>
    public int Eti { get; set; }

    public int BandBancoSangre { get; set; }
}
