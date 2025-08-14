using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

public partial class TblCasos
{
    public int IdCasoIndividual { get; set; }

    public int? IdPersona { get; set; }

    public int? TipoDocumento { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? Tecnico { get; set; }

    public int? ResidenciaIdProvincia { get; set; }

    public int? ResidenciaIdRegion { get; set; }

    public int? ResidenciaIdDistrito { get; set; }

    public int? ResidenciaIdCorregimiento { get; set; }

    public string? ResidenciaDireccion { get; set; }

    public int? AuditoriaUsuario { get; set; }

    public DateTime? AuditoriaFecha { get; set; }

    public string? AuditoriaBrowser { get; set; }

    public string? AuditoriaBrowserVersion { get; set; }

    public string? AuditoriaSistemaOperativo { get; set; }
}
