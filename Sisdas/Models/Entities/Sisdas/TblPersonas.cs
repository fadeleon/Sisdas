using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

public partial class TblPersonas
{
    public int IdPersona { get; set; }

    public sbyte TipoDocumento { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public string? PrimerApellido { get; set; }

    public string? SegundoApellido { get; set; }

    public string? CasadaApellido { get; set; }

    public string? Sexo { get; set; }

    public string? Email { get; set; }

    public string? Telefono { get; set; }

    public string? Movil { get; set; }

    public string? FechaNacimiento { get; set; }

    public int? DomicilioProvincia { get; set; }

    public int? DomicilioRegion { get; set; }

    public int? DomicilioDistrito { get; set; }

    public int? DomicilioCorregimiento { get; set; }

    public string? DomicilioLocalidad { get; set; }

    public string? EdadRegistro { get; set; }

    public int? AuditoriaUsuario { get; set; }

    public DateTime? AuditoriaFecha { get; set; }

    public string? AuditoriaBrowser { get; set; }

    public string? AuditoriaBrowserVersion { get; set; }

    public string? AuditoriaSistemaOperativo { get; set; }

    public virtual ICollection<TblCasosAdmisiones> TblCasosAdmisiones { get; set; } = new List<TblCasosAdmisiones>();

    public virtual ICollection<TblCasosAdmisionesBitacora> TblCasosAdmisionesBitacora { get; set; } = new List<TblCasosAdmisionesBitacora>();
}
