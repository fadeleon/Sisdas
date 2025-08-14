using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Sisdas.Models.Entities.Sisdas;

public partial class DBContextSisdas : DbContext
{
    public DBContextSisdas()
    {
    }

    public DBContextSisdas(DbContextOptions<DBContextSisdas> options)
        : base(options)
    {
    }

    public virtual DbSet<CatCorregimiento> CatCorregimiento { get; set; }

    public virtual DbSet<CatDistrito> CatDistrito { get; set; }

    public virtual DbSet<CatModulos> CatModulos { get; set; }

    public virtual DbSet<CatPais> CatPais { get; set; }

    public virtual DbSet<CatProvincia> CatProvincia { get; set; }

    public virtual DbSet<CatRegionSalud> CatRegionSalud { get; set; }

    public virtual DbSet<CatServicio> CatServicio { get; set; }

    public virtual DbSet<CatTiposDocumentos> CatTiposDocumentos { get; set; }

    public virtual DbSet<CatUnidadNotificadora> CatUnidadNotificadora { get; set; }

    public virtual DbSet<CatUnidadNotificadora2> CatUnidadNotificadora2 { get; set; }

    public virtual DbSet<TblCasos> TblCasos { get; set; }

    public virtual DbSet<TblCasosAdmisiones> TblCasosAdmisiones { get; set; }

    public virtual DbSet<TblCasosAdmisionesBitacora> TblCasosAdmisionesBitacora { get; set; }

    public virtual DbSet<TblPersonas> TblPersonas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=10.130.254.136;database=db_sisdas;user id=u_sisdas;password=9.UWtCR#860*", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.37-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<CatCorregimiento>(entity =>
        {
            entity.HasKey(e => e.IdCorregimiento).HasName("PRIMARY");

            entity
                .ToTable("cat_corregimiento", tb => tb.HasComment("Tabla con los datos de los corregimientos de Panamá"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdDistrito, "fk_distrito");

            entity.Property(e => e.IdCorregimiento).HasColumnName("id_corregimiento");
            entity.Property(e => e.CodRefMinsa)
                .HasMaxLength(10)
                .HasComment("Código de referencia con los catálogos del MINSA.")
                .HasColumnName("cod_ref_minsa");
            entity.Property(e => e.IdDistrito)
                .HasComment("Código del distrito al que pertenece el corregimiento.")
                .HasColumnName("id_distrito");
            entity.Property(e => e.NombreCorregimiento)
                .HasMaxLength(100)
                .HasComment("Nombre del corregimiento de Panamá")
                .HasColumnName("nombre_corregimiento");
        });

        modelBuilder.Entity<CatDistrito>(entity =>
        {
            entity.HasKey(e => e.IdDistrito).HasName("PRIMARY");

            entity
                .ToTable("cat_distrito", tb => tb.HasComment("Tabla con los datos de los distritos de Panamá"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdProvincia, "fk_provincia");

            entity.HasIndex(e => e.IdRegion, "fk_region_distrito");

            entity.Property(e => e.IdDistrito).HasColumnName("id_distrito");
            entity.Property(e => e.CodRefMinsa)
                .HasMaxLength(10)
                .HasComment("Código de referencia con los catálogos del MINSA")
                .HasColumnName("cod_ref_minsa");
            entity.Property(e => e.IdProvincia)
                .HasComment("Provincia a la que pertenece el distrito")
                .HasColumnName("id_provincia");
            entity.Property(e => e.IdRegion)
                .HasDefaultValueSql("'1'")
                .HasColumnName("id_region");
            entity.Property(e => e.NombreDistrito)
                .HasMaxLength(100)
                .HasComment("Nombre del distrito de Panamá")
                .HasColumnName("nombre_distrito");
        });

        modelBuilder.Entity<CatModulos>(entity =>
        {
            entity.HasKey(e => e.IdModulo).HasName("PRIMARY");

            entity
                .ToTable("cat_modulos")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdModulo).HasColumnName("id_modulo");
            entity.Property(e => e.NombreModulo)
                .HasMaxLength(45)
                .HasColumnName("nombre_modulo");
        });

        modelBuilder.Entity<CatPais>(entity =>
        {
            entity.HasKey(e => e.IdPais).HasName("PRIMARY");

            entity
                .ToTable("cat_pais", tb => tb.HasComment("Catalogo de los paises del mundo"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdPais)
                .HasComment("Identificador del pais")
                .HasColumnName("id_pais");
            entity.Property(e => e.NombrePais)
                .HasMaxLength(30)
                .HasComment("Nombre del pais")
                .HasColumnName("nombre_pais");
        });

        modelBuilder.Entity<CatProvincia>(entity =>
        {
            entity.HasKey(e => e.IdProvincia).HasName("PRIMARY");

            entity
                .ToTable("cat_provincia", tb => tb.HasComment("Tabla con los datos de las provincias de Panamá"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdProvincia).HasColumnName("id_provincia");
            entity.Property(e => e.CodRefMinsa)
                .HasMaxLength(10)
                .HasComment("Cóidgo de referencia con catálogo de provincias del MINSA")
                .HasColumnName("cod_ref_minsa");
            entity.Property(e => e.Estatus).HasColumnName("estatus");
            entity.Property(e => e.NombreProvincia)
                .HasMaxLength(100)
                .HasComment("Nombre de las provincias de Panamá")
                .HasColumnName("nombre_provincia");
        });

        modelBuilder.Entity<CatRegionSalud>(entity =>
        {
            entity.HasKey(e => e.IdRegion).HasName("PRIMARY");

            entity
                .ToTable("cat_region_salud", tb => tb.HasComment("Tabla con los datos de las regiones de salud"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdRegion)
                .HasComment("Código correlativo númerico y autoincremental de las regiones de salud del MINSA")
                .HasColumnName("id_region");
            entity.Property(e => e.IdProvincia)
                .HasComment("Identificador de la provincia")
                .HasColumnName("id_provincia");
            entity.Property(e => e.NombreRegion)
                .HasMaxLength(100)
                .HasComment("Nombre de la Región de Salud")
                .HasColumnName("nombre_region");
        });

        modelBuilder.Entity<CatServicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PRIMARY");

            entity
                .ToTable("cat_servicio", tb => tb.HasComment("Tabla que contiene el nombre del servicio de salud"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdServicio)
                .HasComment("Identificador del servicio de salud")
                .HasColumnName("id_servicio");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(100)
                .HasComment("Nombre del servicio de salud")
                .HasColumnName("nombre_servicio");
        });

        modelBuilder.Entity<CatTiposDocumentos>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PRIMARY");

            entity
                .ToTable("cat_tipos_documentos")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdTipoDocumento).HasColumnName("id_tipo_documento");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NombreDocumento)
                .HasMaxLength(45)
                .HasColumnName("nombre_documento");
        });

        modelBuilder.Entity<CatUnidadNotificadora>(entity =>
        {
            entity.HasKey(e => e.IdUn).HasName("PRIMARY");

            entity
                .ToTable("cat_unidad_notificadora", tb => tb.HasComment("Tabla con los datos de la Unidad Notificadora"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdCorregimiento, "fk_corregimiento");

            entity.HasIndex(e => e.IdRegion, "fk_region");

            entity.HasIndex(e => e.IdtipoInstalacion, "fk_tipo_unidad");

            entity.Property(e => e.IdUn).HasColumnName("id_un");
            entity.Property(e => e.BandBancoSangre).HasColumnName("band_banco_sangre");
            entity.Property(e => e.Centinela)
                .HasComment("1= Es sitio centinela de Influenza")
                .HasColumnName("CENTINELA");
            entity.Property(e => e.CodRefMinsa)
                .HasMaxLength(10)
                .HasComment("Código referencia del MINSA")
                .HasColumnName("cod_ref_minsa");
            entity.Property(e => e.Eti)
                .HasComment("1= Unidad centinela de Influenza que vigila ETI")
                .HasColumnName("ETI");
            entity.Property(e => e.IdCorregimiento)
                .HasComment("Código del corregimiento en el que está ubicada la unidad notificadora")
                .HasColumnName("id_corregimiento");
            entity.Property(e => e.IdRegion)
                .HasComment("Código de la región de salud a la que pertenece.")
                .HasColumnName("id_region");
            entity.Property(e => e.IdtipoInstalacion)
                .HasComment("Identificador del tipo de instalacion")
                .HasColumnName("idtipo_instalacion");
            entity.Property(e => e.Irag)
                .HasComment("1= Unidad centinela de Influenza que vigila IRAG")
                .HasColumnName("IRAG");
            entity.Property(e => e.NombreUn)
                .HasMaxLength(100)
                .HasComment("Nombre de la Unidad Notificadora")
                .HasColumnName("nombre_un");
            entity.Property(e => e.SectorUn)
                .HasComment("Sector al que pertenece la Unidad notificadora:\n1 = MINSA\n2 = Caja del Seguro Social\n3 = Privado\n4 = ONG\n5 = Cooperación Externa\n6 = Otros")
                .HasColumnName("sector_un");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("Estado del registro, 1 = Habilitado y 0 = Deshabilitado")
                .HasColumnName("status");
        });

        modelBuilder.Entity<CatUnidadNotificadora2>(entity =>
        {
            entity.HasKey(e => e.IdUn).HasName("PRIMARY");

            entity
                .ToTable("cat_unidad_notificadora2", tb => tb.HasComment("Tabla con los datos de la Unidad Notificadora"))
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.IdUn).HasColumnName("id_un");
            entity.Property(e => e.BandBancoSangre).HasColumnName("band_banco_sangre");
            entity.Property(e => e.Centinela)
                .HasComment("1= Es sitio centinela de Influenza")
                .HasColumnName("CENTINELA");
            entity.Property(e => e.CodRefMinsa)
                .HasMaxLength(10)
                .HasComment("Código referencia del MINSA")
                .HasColumnName("cod_ref_minsa");
            entity.Property(e => e.Eti)
                .HasComment("1= Unidad centinela de Influenza que vigila ETI")
                .HasColumnName("ETI");
            entity.Property(e => e.Irag)
                .HasComment("1= Unidad centinela de Influenza que vigila IRAG")
                .HasColumnName("IRAG");
            entity.Property(e => e.NombreUn)
                .HasMaxLength(100)
                .HasComment("Nombre de la Unidad Notificadora")
                .HasColumnName("nombre_un");
            entity.Property(e => e.SectorUn)
                .HasComment("Sector al que pertenece la Unidad notificadora:\n1 = MINSA\n2 = Caja del Seguro Social\n3 = Privado\n4 = ONG\n5 = Cooperación Externa\n6 = Otros")
                .HasColumnName("sector_un");
            entity.Property(e => e.Status)
                .HasDefaultValueSql("'1'")
                .HasComment("Estado del registro, 1 = Habilitado y 0 = Deshabilitado")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblCasos>(entity =>
        {
            entity.HasKey(e => e.IdCasoIndividual).HasName("PRIMARY");

            entity.ToTable("tbl_casos");

            entity.HasIndex(e => e.IdPersona, "id_persona").IsUnique();

            entity.Property(e => e.IdCasoIndividual).HasColumnName("id_caso_individual");
            entity.Property(e => e.AuditoriaBrowser)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaBrowserVersion)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser_version")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaFecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("auditoria_fecha");
            entity.Property(e => e.AuditoriaSistemaOperativo)
                .HasMaxLength(45)
                .HasColumnName("auditoria_sistema_operativo")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaUsuario).HasColumnName("auditoria_usuario");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(45)
                .HasColumnName("numero_identificacion")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.ResidenciaDireccion)
                .HasColumnType("text")
                .HasColumnName("residenciaDireccion");
            entity.Property(e => e.ResidenciaIdCorregimiento).HasColumnName("residenciaIdCorregimiento");
            entity.Property(e => e.ResidenciaIdDistrito).HasColumnName("residenciaIdDistrito");
            entity.Property(e => e.ResidenciaIdProvincia).HasColumnName("residenciaIdProvincia");
            entity.Property(e => e.ResidenciaIdRegion).HasColumnName("residenciaIdRegion");
            entity.Property(e => e.Tecnico)
                .HasMaxLength(45)
                .HasColumnName("tecnico");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipo_documento");
        });

        modelBuilder.Entity<TblCasosAdmisiones>(entity =>
        {
            entity.HasKey(e => e.IdAdmision).HasName("PRIMARY");

            entity.ToTable("tbl_casos_admisiones");

            entity.HasIndex(e => e.IdCasoIndividual, "FK_EXPEDIENTE");

            entity.HasIndex(e => e.IdPersona, "FK_PERSONA");

            entity.Property(e => e.IdAdmision).HasColumnName("id_admision");
            entity.Property(e => e.AnticonceptivoPrevio).HasColumnName("anticonceptivo_previo");
            entity.Property(e => e.AnticonceptivoPrevioTipo).HasColumnName("anticonceptivo_previo_tipo");
            entity.Property(e => e.AuditoriaBrowser)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaBrowserVersion)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser_version")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaFecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("auditoria_fecha");
            entity.Property(e => e.AuditoriaSistemaOperativo)
                .HasMaxLength(45)
                .HasColumnName("auditoria_sistema_operativo")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaUsuario).HasColumnName("auditoria_usuario");
            entity.Property(e => e.CantidadAbortos).HasColumnName("cantidad_abortos");
            entity.Property(e => e.CantidadNacidosVivos).HasColumnName("cantidad_nacidos_vivos");
            entity.Property(e => e.IdCasoIndividual).HasColumnName("id_caso_individual");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.InstalacionSalud).HasColumnName("instalacion_salud");
            entity.Property(e => e.MetodoInicio).HasColumnName("metodo_inicio");
            entity.Property(e => e.MetodoInicioFecha).HasColumnName("metodo_inicio_fecha");
            entity.Property(e => e.MetodoInicioMomento).HasColumnName("metodo_inicio_momento");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(45)
                .HasColumnName("numero_identificacion")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Profesional).HasColumnName("profesional");
            entity.Property(e => e.ProfesionalCedula)
                .HasMaxLength(15)
                .HasColumnName("profesional_cedula");
            entity.Property(e => e.ProfesionalNombre)
                .HasMaxLength(60)
                .HasColumnName("profesional_nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Tecnico)
                .HasMaxLength(45)
                .HasColumnName("tecnico")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.TipoConsulta).HasColumnName("tipo_consulta");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipo_documento");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblCasosAdmisiones)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("tbl_casos_admisiones_ibfk_1");
        });

        modelBuilder.Entity<TblCasosAdmisionesBitacora>(entity =>
        {
            entity.HasKey(e => e.IdAdmision).HasName("PRIMARY");

            entity.ToTable("tbl_casos_admisiones_bitacora");

            entity.HasIndex(e => e.IdCasoIndividual, "FK_EXPEDIENTE");

            entity.HasIndex(e => e.IdPersona, "FK_PERSONA");

            entity.Property(e => e.IdAdmision).HasColumnName("id_admision");
            entity.Property(e => e.AnticonceptivoPrevio).HasColumnName("anticonceptivo_previo");
            entity.Property(e => e.AnticonceptivoPrevioTipo).HasColumnName("anticonceptivo_previo_tipo");
            entity.Property(e => e.AuditoriaBrowser)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaBrowserVersion)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser_version")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaFecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("auditoria_fecha");
            entity.Property(e => e.AuditoriaSistemaOperativo)
                .HasMaxLength(45)
                .HasColumnName("auditoria_sistema_operativo")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.AuditoriaUsuario).HasColumnName("auditoria_usuario");
            entity.Property(e => e.CantidadAbortos).HasColumnName("cantidad_abortos");
            entity.Property(e => e.CantidadNacidosVivos).HasColumnName("cantidad_nacidos_vivos");
            entity.Property(e => e.IdCasoIndividual).HasColumnName("id_caso_individual");
            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.InstalacionSalud).HasColumnName("instalacion_salud");
            entity.Property(e => e.MetodoInicio).HasColumnName("metodo_inicio");
            entity.Property(e => e.MetodoInicioFecha).HasColumnName("metodo_inicio_fecha");
            entity.Property(e => e.MetodoInicioMomento).HasColumnName("metodo_inicio_momento");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(45)
                .HasColumnName("numero_identificacion")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Profesional).HasColumnName("profesional");
            entity.Property(e => e.ProfesionalCedula)
                .HasMaxLength(15)
                .HasColumnName("profesional_cedula");
            entity.Property(e => e.ProfesionalNombre)
                .HasMaxLength(60)
                .HasColumnName("profesional_nombre")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.Tecnico)
                .HasMaxLength(45)
                .HasColumnName("tecnico")
                .UseCollation("utf8mb3_general_ci")
                .HasCharSet("utf8mb3");
            entity.Property(e => e.TipoConsulta).HasColumnName("tipo_consulta");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipo_documento");

            entity.HasOne(d => d.IdPersonaNavigation).WithMany(p => p.TblCasosAdmisionesBitacora)
                .HasForeignKey(d => d.IdPersona)
                .HasConstraintName("tbl_casos_admisiones_bitacora_ibfk_1");
        });

        modelBuilder.Entity<TblPersonas>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PRIMARY");

            entity
                .ToTable("tbl_personas")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.DomicilioCorregimiento, "fk_corregimiento_idx");

            entity.HasIndex(e => e.DomicilioDistrito, "fk_distrito_idx");

            entity.HasIndex(e => e.DomicilioProvincia, "fk_provincia_idx");

            entity.HasIndex(e => e.DomicilioRegion, "fk_region_idx");

            entity.HasIndex(e => e.NumeroIdentificacion, "numero_identificacion").IsUnique();

            entity.Property(e => e.IdPersona).HasColumnName("id_persona");
            entity.Property(e => e.AuditoriaBrowser)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser");
            entity.Property(e => e.AuditoriaBrowserVersion)
                .HasMaxLength(45)
                .HasColumnName("auditoria_browser_version");
            entity.Property(e => e.AuditoriaFecha)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("auditoria_fecha");
            entity.Property(e => e.AuditoriaSistemaOperativo)
                .HasMaxLength(45)
                .HasColumnName("auditoria_sistema_operativo");
            entity.Property(e => e.AuditoriaUsuario).HasColumnName("auditoria_usuario");
            entity.Property(e => e.CasadaApellido)
                .HasMaxLength(45)
                .HasColumnName("casada_apellido");
            entity.Property(e => e.DomicilioCorregimiento).HasColumnName("domicilio_corregimiento");
            entity.Property(e => e.DomicilioDistrito).HasColumnName("domicilio_distrito");
            entity.Property(e => e.DomicilioLocalidad)
                .HasColumnType("text")
                .HasColumnName("domicilio_localidad");
            entity.Property(e => e.DomicilioProvincia).HasColumnName("domicilio_provincia");
            entity.Property(e => e.DomicilioRegion).HasColumnName("domicilio_region");
            entity.Property(e => e.EdadRegistro)
                .HasMaxLength(45)
                .HasColumnName("edad_registro");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.FechaNacimiento)
                .HasMaxLength(45)
                .HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Movil)
                .HasMaxLength(45)
                .HasColumnName("movil");
            entity.Property(e => e.NumeroIdentificacion)
                .HasMaxLength(45)
                .HasColumnName("numero_identificacion");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(45)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.PrimerNombre)
                .HasMaxLength(45)
                .HasColumnName("primer_nombre");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(45)
                .HasColumnName("segundo_apellido");
            entity.Property(e => e.SegundoNombre)
                .HasMaxLength(45)
                .HasColumnName("segundo_nombre");
            entity.Property(e => e.Sexo)
                .HasMaxLength(45)
                .HasColumnName("sexo");
            entity.Property(e => e.Telefono)
                .HasMaxLength(45)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoDocumento).HasColumnName("tipo_documento");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
