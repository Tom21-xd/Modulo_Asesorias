using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Modulo_Asesorias.Models;

public partial class AsesoriaContext : DbContext
{
    public AsesoriaContext()
    {
    }

    public AsesoriaContext(DbContextOptions<AsesoriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendum> Agenda { get; set; }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Asignaturaplandeestudio> Asignaturaplandeestudios { get; set; }

    public virtual DbSet<Facultad> Facultads { get; set; }

    public virtual DbSet<Grupo> Grupos { get; set; }

    public virtual DbSet<Impartir> Impartirs { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<Plandeestudio> Plandeestudios { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Sala> Salas { get; set; }

    public virtual DbSet<Tipodocumento> Tipodocumentos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vistum> Vista { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=asesoria;uid=root;password=0518", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Agendum>(entity =>
        {
            entity.HasKey(e => e.IdAgenda).HasName("PRIMARY");

            entity.ToTable("agenda");

            entity.HasIndex(e => e.FkIdSala, "16_idx");

            entity.HasIndex(e => e.FkIdProfesor, "17_idx");

            entity.HasIndex(e => e.FkIdEstudiante, "18_idx");

            entity.Property(e => e.IdAgenda).HasColumnName("ID_AGENDA");
            entity.Property(e => e.DiaAgenda)
                .HasMaxLength(45)
                .HasColumnName("DIA_AGENDA");
            entity.Property(e => e.FechaAgenda).HasColumnName("FECHA_AGENDA");
            entity.Property(e => e.FkIdEstudiante).HasColumnName("FK_ID_ESTUDIANTE");
            entity.Property(e => e.FkIdProfesor).HasColumnName("FK_ID_PROFESOR");
            entity.Property(e => e.FkIdSala).HasColumnName("FK_ID_SALA");
            entity.Property(e => e.HorafinAgenda)
                .HasColumnType("time")
                .HasColumnName("HORAFIN_AGENDA");
            entity.Property(e => e.HorainicioAgenda)
                .HasColumnType("time")
                .HasColumnName("HORAINICIO_AGENDA");
            entity.Property(e => e.NumeroAgenda).HasColumnName("NUMERO_AGENDA");

            entity.HasOne(d => d.FkIdEstudianteNavigation).WithMany(p => p.AgendumFkIdEstudianteNavigations)
                .HasForeignKey(d => d.FkIdEstudiante)
                .HasConstraintName("18");

            entity.HasOne(d => d.FkIdProfesorNavigation).WithMany(p => p.AgendumFkIdProfesorNavigations)
                .HasForeignKey(d => d.FkIdProfesor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("17");

            entity.HasOne(d => d.FkIdSalaNavigation).WithMany(p => p.Agenda)
                .HasForeignKey(d => d.FkIdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("16");
        });

        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.IdAsignatura).HasName("PRIMARY");

            entity.ToTable("asignatura");

            entity.Property(e => e.IdAsignatura).HasColumnName("ID_ASIGNATURA");
            entity.Property(e => e.EstadoAsignatura)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_ASIGNATURA");
            entity.Property(e => e.NombreAsignatura)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE_ASIGNATURA");
        });

        modelBuilder.Entity<Asignaturaplandeestudio>(entity =>
        {
            entity.HasKey(e => new { e.FkIdAsignatura, e.FkIdPlandeestudio })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("asignaturaplandeestudio");

            entity.HasIndex(e => e.FkIdAsignatura, "13_idx");

            entity.HasIndex(e => e.FkIdPlandeestudio, "14_idx");

            entity.Property(e => e.FkIdAsignatura).HasColumnName("FK_ID_ASIGNATURA");
            entity.Property(e => e.FkIdPlandeestudio).HasColumnName("FK_ID_PLANDEESTUDIO");
            entity.Property(e => e.SemestreAsignaturaplandeestudio).HasColumnName("SEMESTRE_ASIGNATURAPLANDEESTUDIO");

            entity.HasOne(d => d.FkIdAsignaturaNavigation).WithMany(p => p.Asignaturaplandeestudios)
                .HasForeignKey(d => d.FkIdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("13");

            entity.HasOne(d => d.FkIdPlandeestudioNavigation).WithMany(p => p.Asignaturaplandeestudios)
                .HasForeignKey(d => d.FkIdPlandeestudio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("14");
        });

        modelBuilder.Entity<Facultad>(entity =>
        {
            entity.HasKey(e => e.IdFacultad).HasName("PRIMARY");

            entity.ToTable("facultad");

            entity.Property(e => e.IdFacultad).HasColumnName("ID_FACULTAD");
            entity.Property(e => e.EstadoFacultad)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_FACULTAD");
            entity.Property(e => e.NombreFacultad)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE_FACULTAD");
        });

        modelBuilder.Entity<Grupo>(entity =>
        {
            entity.HasKey(e => e.IdGrupo).HasName("PRIMARY");

            entity.ToTable("grupo");

            entity.HasIndex(e => e.FkIdAsignatura, "10_idx");

            entity.HasIndex(e => e.FkIdSala, "15_idx");

            entity.Property(e => e.IdGrupo).HasColumnName("ID_GRUPO");
            entity.Property(e => e.EstadoGrupo)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_GRUPO");
            entity.Property(e => e.FkIdAsignatura).HasColumnName("FK_ID_ASIGNATURA");
            entity.Property(e => e.FkIdSala).HasColumnName("FK_ID_SALA");
            entity.Property(e => e.NumeroGrupo).HasColumnName("NUMERO_GRUPO");

            entity.HasOne(d => d.FkIdAsignaturaNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.FkIdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("10");

            entity.HasOne(d => d.FkIdSalaNavigation).WithMany(p => p.Grupos)
                .HasForeignKey(d => d.FkIdSala)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("15");
        });

        modelBuilder.Entity<Impartir>(entity =>
        {
            entity.HasKey(e => new { e.FkIdUsuario, e.FkIdAsignatura })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("impartir");

            entity.HasIndex(e => e.FkIdUsuario, "6_idx");

            entity.HasIndex(e => e.FkIdAsignatura, "7_idx");

            entity.Property(e => e.FkIdUsuario).HasColumnName("FK_ID_USUARIO");
            entity.Property(e => e.FkIdAsignatura).HasColumnName("FK_ID_ASIGNATURA");
            entity.Property(e => e.EstadoImpartir)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_IMPARTIR");

            entity.HasOne(d => d.FkIdAsignaturaNavigation).WithMany(p => p.Impartirs)
                .HasForeignKey(d => d.FkIdAsignatura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("7");

            entity.HasOne(d => d.FkIdUsuarioNavigation).WithMany(p => p.Impartirs)
                .HasForeignKey(d => d.FkIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("6");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => new { e.FkIdGrupo, e.FkIdUsuario })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("matricula");

            entity.HasIndex(e => e.FkIdGrupo, "8_idx");

            entity.HasIndex(e => e.FkIdUsuario, "9_idx");

            entity.Property(e => e.FkIdGrupo).HasColumnName("FK_ID_GRUPO");
            entity.Property(e => e.FkIdUsuario).HasColumnName("FK_ID_USUARIO");
            entity.Property(e => e.FechaMatricula).HasColumnName("FECHA_MATRICULA");

            entity.HasOne(d => d.FkIdGrupoNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.FkIdGrupo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("8");

            entity.HasOne(d => d.FkIdUsuarioNavigation).WithMany(p => p.Matriculas)
                .HasForeignKey(d => d.FkIdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("9");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PRIMARY");

            entity.ToTable("permiso");

            entity.Property(e => e.IdPermiso).HasColumnName("ID_PERMISO");
            entity.Property(e => e.EstadoPermiso)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_PERMISO");
            entity.Property(e => e.NombrePermiso)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE_PERMISO");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.IdPersona).HasName("PRIMARY");

            entity.ToTable("persona");

            entity.HasIndex(e => e.FkIdTipodocumento, "5_idx");

            entity.Property(e => e.IdPersona)
                .ValueGeneratedNever()
                .HasColumnName("ID_PERSONA");
            entity.Property(e => e.Apellido1Persona)
                .HasMaxLength(45)
                .HasColumnName("APELLIDO1_PERSONA");
            entity.Property(e => e.Apellido2Persona)
                .HasMaxLength(45)
                .HasColumnName("APELLIDO2_PERSONA");
            entity.Property(e => e.FkIdTipodocumento).HasColumnName("FK_ID_TIPODOCUMENTO");
            entity.Property(e => e.Nombre1Persona)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE1_PERSONA");
            entity.Property(e => e.Nombre2Persona)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE2_PERSONA");

            entity.HasOne(d => d.FkIdTipodocumentoNavigation).WithMany(p => p.Personas)
                .HasForeignKey(d => d.FkIdTipodocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("5");
        });

        modelBuilder.Entity<Plandeestudio>(entity =>
        {
            entity.HasKey(e => e.IdPlandeestudio).HasName("PRIMARY");

            entity.ToTable("plandeestudio");

            entity.HasIndex(e => e.FkIdPrograma, "12_idx");

            entity.Property(e => e.IdPlandeestudio).HasColumnName("ID_PLANDEESTUDIO");
            entity.Property(e => e.EstadoPlandeestudio)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_PLANDEESTUDIO");
            entity.Property(e => e.FkIdPrograma).HasColumnName("FK_ID_PROGRAMA");
            entity.Property(e => e.NombrePlandeestudio)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE_PLANDEESTUDIO");

            entity.HasOne(d => d.FkIdProgramaNavigation).WithMany(p => p.Plandeestudios)
                .HasForeignKey(d => d.FkIdPrograma)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("12");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.HasKey(e => e.IdPrograma).HasName("PRIMARY");

            entity.ToTable("programa");

            entity.HasIndex(e => e.FkIdFacultad, "11_idx");

            entity.Property(e => e.IdPrograma).HasColumnName("ID_PROGRAMA");
            entity.Property(e => e.EstadoPrograma)
                .HasMaxLength(45)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_PROGRAMA");
            entity.Property(e => e.FkIdFacultad).HasColumnName("FK_ID_FACULTAD");
            entity.Property(e => e.NombrePrograma)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE_PROGRAMA");

            entity.HasOne(d => d.FkIdFacultadNavigation).WithMany(p => p.Programas)
                .HasForeignKey(d => d.FkIdFacultad)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("11");
        });

        modelBuilder.Entity<Sala>(entity =>
        {
            entity.HasKey(e => e.IdSala).HasName("PRIMARY");

            entity.ToTable("sala");

            entity.Property(e => e.IdSala).HasColumnName("ID_SALA");
            entity.Property(e => e.BloqueSala)
                .HasMaxLength(45)
                .HasColumnName("BLOQUE_SALA");
            entity.Property(e => e.NumeroSala)
                .HasMaxLength(45)
                .HasColumnName("NUMERO_SALA");
        });

        modelBuilder.Entity<Tipodocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipodocumento).HasName("PRIMARY");

            entity.ToTable("tipodocumento");

            entity.Property(e => e.IdTipodocumento).HasColumnName("ID_TIPODOCUMENTO");
            entity.Property(e => e.EstadoTipodocumento)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_TIPODOCUMENTO");
            entity.Property(e => e.NombreTipodocumento)
                .HasMaxLength(45)
                .HasColumnName("NOMBRE_TIPODOCUMENTO");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.FkIdPersona, "3_idx");

            entity.HasIndex(e => e.FkIdPermiso, "4_idx");

            entity.Property(e => e.IdUsuario).HasColumnName("ID_USUARIO");
            entity.Property(e => e.ContraseniaUsuario)
                .HasMaxLength(45)
                .HasColumnName("CONTRASENIA_USUARIO");
            entity.Property(e => e.CorreoUsuario)
                .HasMaxLength(45)
                .HasColumnName("CORREO_USUARIO");
            entity.Property(e => e.EstadoUsuario)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_USUARIO");
            entity.Property(e => e.FkIdPermiso).HasColumnName("FK_ID_PERMISO");
            entity.Property(e => e.FkIdPersona).HasColumnName("FK_ID_PERSONA");

            entity.HasOne(d => d.FkIdPermisoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkIdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("4");

            entity.HasOne(d => d.FkIdPersonaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.FkIdPersona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("3");
        });

        modelBuilder.Entity<Vistum>(entity =>
        {
            entity.HasKey(e => e.IdVista).HasName("PRIMARY");

            entity.ToTable("vista");

            entity.HasIndex(e => e.FkIdPermiso, "2_idx");

            entity.Property(e => e.IdVista).HasColumnName("ID_VISTA");
            entity.Property(e => e.AccionVista)
                .HasMaxLength(45)
                .HasColumnName("ACCION_VISTA");
            entity.Property(e => e.ControladorVista)
                .HasMaxLength(45)
                .HasColumnName("CONTROLADOR_VISTA");
            entity.Property(e => e.EstadoVista)
                .HasDefaultValueSql("'1'")
                .HasColumnName("ESTADO_VISTA");
            entity.Property(e => e.FkIdPermiso).HasColumnName("FK_ID_PERMISO");

            entity.HasOne(d => d.FkIdPermisoNavigation).WithMany(p => p.Vista)
                .HasForeignKey(d => d.FkIdPermiso)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("2");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
