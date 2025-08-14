using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

/// <summary>
/// Tabla con los datos de la Unidad Notificadora
/// </summary>
public partial class CatUnidadNotificadora2
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
    /// Código referencia del MINSA
    /// </summary>
    public string? CodRefMinsa { get; set; }

    /// <summary>
    /// Estado del registro, 1 = Habilitado y 0 = Deshabilitado
    /// </summary>
    public int? Status { get; set; }

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
