using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Construccion_Danna.Models;

public partial class BdAsesoriaContext : DbContext
{
    public BdAsesoriaContext()
    {
    }
    public BdAsesoriaContext(DbContextOptions<BdAsesoriaContext> options): base(options)
    {
    }

    public virtual DbSet<Asesorium> Asesoria { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Peragendaase> Peragendaases { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Progcontienmat> Progcontienmats { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Tipodocumento> Tipodocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vista> Vistas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=bd_asesoria;uid=root;password=0518", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Asesorium>(entity =>
        {
            entity.HasKey(e => e.AseId).HasName("PRIMARY");

            entity.ToTable("asesoria");

            entity.HasIndex(e => e.FkUsuId, "3_idx");

            entity.Property(e => e.AseId).HasColumnName("ASE_ID");
            entity.Property(e => e.AseDia)
                .HasMaxLength(45)
                .HasColumnName("ASE_Dia");
            entity.Property(e => e.AseHoraFin)
                .HasColumnType("time")
                .HasColumnName("ASE_Hora_Fin");
            entity.Property(e => e.AseHoraInicio)
                .HasColumnType("time")
                .HasColumnName("ASE_Hora_Inicio");
            entity.Property(e => e.AseNumero).HasColumnName("ASE_Numero");
            entity.Property(e => e.FkUsuId).HasColumnName("FK_USU_ID");

            entity.HasOne(d => d.FkUsu).WithMany(p => p.Asesoria)
                .HasForeignKey(d => d.FkUsuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("3");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.AsiId).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.AsiId).HasColumnName("ASI_ID");
            entity.Property(e => e.AsiEstado).HasColumnName("ASI_Estado");
            entity.Property(e => e.AsiNombre)
                .HasMaxLength(45)
                .HasColumnName("ASI_Nombre");

            entity.HasMany(d => d.FkUsus).WithMany(p => p.FkAsis)
                .UsingEntity<Dictionary<string, object>>(
                    "Asignaturausuario",
                    r => r.HasOne<Usuario>().WithMany()
                        .HasForeignKey("FkUsuId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("6"),
                    l => l.HasOne<Asignatura>().WithMany()
                        .HasForeignKey("FkAsiId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("5"),
                    j =>
                    {
                        j.HasKey("FkAsiId", "FkUsuId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("asignaturausuario");
                        j.HasIndex(new[] { "FkAsiId" }, "5_idx");
                        j.HasIndex(new[] { "FkUsuId" }, "6_idx");
                        j.IndexerProperty<int>("FkAsiId").HasColumnName("FK_ASI_ID");
                        j.IndexerProperty<int>("FkUsuId").HasColumnName("FK_USU_ID");
                    });
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.GrupId).HasName("PRIMARY");

            entity.ToTable("grupo");

            entity.HasIndex(e => e.FkAsiId, "4_idx");

            entity.Property(e => e.GrupId).HasColumnName("GRUP_ID");
            entity.Property(e => e.FkAsiId).HasColumnName("FK_ASI_ID");
            entity.Property(e => e.GrupEstado).HasColumnName("GRUP_Estado");
            entity.Property(e => e.GrupNumeroGrupo)
                .HasMaxLength(45)
                .HasColumnName("GRUP_NumeroGrupo");

            entity.HasOne(d => d.FkAsi).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.FkAsiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("4");
        });

        modelBuilder.Entity<Peragendaase>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("peragendaase");

            entity.HasIndex(e => e.FkUsuId, "2_idx");

            entity.HasIndex(e => e.AseAsesoria, "FkAseAgenda_idx");

            entity.Property(e => e.AseAsesoria).HasColumnName("ASE_Asesoria");
            entity.Property(e => e.FkUsuId).HasColumnName("FK_USU_ID");

            entity.HasOne(d => d.AseAsesoriaNavigation).WithMany()
                .HasForeignKey(d => d.AseAsesoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkAseAgenda");

            entity.HasOne(d => d.FkUsu).WithMany()
                .HasForeignKey(d => d.FkUsuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("2");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.PermId).HasName("PRIMARY");

            entity.ToTable("permiso");

            entity.Property(e => e.PermId).HasColumnName("PERM_ID");
            entity.Property(e => e.PermEstado).HasColumnName("PERM_Estado");
            entity.Property(e => e.PermPermiso)
                .HasMaxLength(45)
                .HasColumnName("PERM_Permiso");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.PerCodigo).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.FktipdocId, "fkTipDocPer_idx");

            entity.Property(e => e.PerCodigo)
                .ValueGeneratedNever()
                .HasColumnName("PER_Codigo");
            entity.Property(e => e.FktipdocId).HasColumnName("FKTIPDOC_ID");
            entity.Property(e => e.PerEstado).HasColumnName("PER_Estado");
            entity.Property(e => e.PerPrimerApellido)
                .HasMaxLength(45)
                .HasColumnName("PER_PrimerApellido");
            entity.Property(e => e.PerPrimerNombre)
                .HasMaxLength(45)
                .HasColumnName("PER_PrimerNombre");
            entity.Property(e => e.PerSegundoApellido)
                .HasMaxLength(45)
                .HasColumnName("PER_SegundoApellido");
            entity.Property(e => e.PerSegundoNombre)
                .HasMaxLength(45)
                .HasColumnName("PER_SegundoNombre");

            entity.HasOne(d => d.Fktipdoc).WithMany(p => p.Personas)
                .HasForeignKey(d => d.FktipdocId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkTipDocPer");
        });

        modelBuilder.Entity<Progcontienmat>(entity =>
        {
            entity.HasKey(e => new { e.ProgId, e.AsiId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("progcontienmat");

            entity.HasIndex(e => e.AsiId, "FkMatCont_idx");

            entity.Property(e => e.ProgId)
                .ValueGeneratedOnAdd()
                .HasColumnName("PROG_ID");
            entity.Property(e => e.AsiId).HasColumnName("ASI_ID");

            entity.HasOne(d => d.Asi).WithMany(p => p.Progcontienmats)
                .HasForeignKey(d => d.AsiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkMatCont");

            entity.HasOne(d => d.Prog).WithMany(p => p.Progcontienmats)
                .HasForeignKey(d => d.ProgId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkProCont");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.ProgId).HasName("PRIMARY");

            entity.ToTable("programa");

            entity.Property(e => e.ProgId).HasColumnName("PROG_ID");
            entity.Property(e => e.ProgEstado).HasColumnName("PROG_Estado");
            entity.Property(e => e.ProgNombre)
                .HasMaxLength(45)
                .HasColumnName("PROG_Nombre");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("PRIMARY");

            entity.ToTable("sala");

            entity.HasIndex(e => e.PkAsesoria, "PK_Sala _idx");

            entity.Property(e => e.IdSala).HasColumnName("Id_Sala");
            entity.Property(e => e.BloqueSala)
                .HasMaxLength(45)
                .HasColumnName("Bloque_Sala");
            entity.Property(e => e.NumeroSala).HasColumnName("Numero_Sala");
            entity.Property(e => e.PkAsesoria).HasColumnName("PK_Asesoria");

            entity.HasOne(d => d.PkAsesoriaNavigation).WithMany(p => p.Salas)
                .HasForeignKey(d => d.PkAsesoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("PK_Sala ");
        });

        modelBuilder.Entity<Tipodocumento>(entity =>
        {
            entity.HasKey(e => e.TipdocId).HasName("PRIMARY");

            entity.ToTable("tipodocumento");

            entity.Property(e => e.TipdocId).HasColumnName("TIPDOC_ID");
            entity.Property(e => e.TipdocEstado).HasColumnName("TIPDOC_Estado");
            entity.Property(e => e.TipdocTipoDocumento)
                .HasMaxLength(45)
                .HasColumnName("TIPDOC_TipoDocumento");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UsuId).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.FkpermisoId, "FKPermiso_idx");

            entity.HasIndex(e => e.Fkpersona, "FK_PER_Codigo");

            entity.HasIndex(e => e.UsuCorreo, "USU_Correo_UNIQUE").IsUnique();

            entity.Property(e => e.UsuId).HasColumnName("USU_ID");
            entity.Property(e => e.FkpermisoId).HasColumnName("FKPermiso_ID");
            entity.Property(e => e.Fkpersona).HasColumnName("FKPersona");
            entity.Property(e => e.UsuCorreo)
                .HasMaxLength(45)
                .HasColumnName("USU_Correo");
            entity.Property(e => e.UsuEstado).HasColumnName("USU_Estado");
            entity.Property(e => e.UsuToken)
                .HasMaxLength(45)
                .HasColumnName("USU_token");

            entity.HasOne(d => d.Fkpermiso).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkpermisoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKPermiso");

            entity.HasOne(d => d.FkpersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Fkpersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKCodigo");
        });

        modelBuilder.Entity<Vista>(entity =>
        {
            entity.HasKey(e => e.VisId).HasName("PRIMARY");

            entity.ToTable("vistas");

            entity.HasIndex(e => e.FkpermId, "FkPerVis_idx");

            entity.Property(e => e.VisId).HasColumnName("VIS_ID");
            entity.Property(e => e.FkpermId).HasColumnName("FKPERM_ID");
            entity.Property(e => e.VisAccion)
                .HasMaxLength(45)
                .HasColumnName("VIS_Accion");
            entity.Property(e => e.VisControlador)
                .HasMaxLength(45)
                .HasColumnName("VIS_Controlador");
            entity.Property(e => e.VisEstado).HasColumnName("VIS_Estado");

            entity.HasOne(d => d.Fkperm).WithMany(p => p.Vista)
                .HasForeignKey(d => d.FkpermId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FkPerVis");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
