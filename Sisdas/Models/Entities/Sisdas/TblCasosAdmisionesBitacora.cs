using System;
using System.Collections.Generic;

namespace Sisdas.Models.Entities.Sisdas;

public partial class TblCasosAdmisionesBitacora
{
    public int IdAdmision { get; set; }

    public int IdCasoIndividual { get; set; }

    public int? IdPersona { get; set; }

    public int? TipoDocumento { get; set; }

    public string? NumeroIdentificacion { get; set; }

    public string? Tecnico { get; set; }

    public int? TipoConsulta { get; set; }

    public int? CantidadNacidosVivos { get; set; }

    public int? CantidadAbortos { get; set; }

    public int? AnticonceptivoPrevio { get; set; }

    public int? AnticonceptivoPrevioTipo { get; set; }

    public int? MetodoInicio { get; set; }

    public DateOnly? MetodoInicioFecha { get; set; }

    public int? MetodoInicioMomento { get; set; }

    public int? Profesional { get; set; }

    public string? ProfesionalNombre { get; set; }

    public string? ProfesionalCedula { get; set; }

    public int? InstalacionSalud { get; set; }

    public int? AuditoriaUsuario { get; set; }

    public DateTime? AuditoriaFecha { get; set; }

    public string? AuditoriaBrowser { get; set; }

    public string? AuditoriaBrowserVersion { get; set; }

    public string? AuditoriaSistemaOperativo { get; set; }

    public virtual TblPersonas? IdPersonaNavigation { get; set; }
}
