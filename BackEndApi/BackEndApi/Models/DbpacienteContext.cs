using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackEndApi.Models;

public partial class DbpacienteContext : DbContext
{
    public DbpacienteContext()
    {
    }

    public DbpacienteContext(DbContextOptions<DbpacienteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<TipoIdentificacion> TipoIdentificacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.IdPaciente).HasName("PK__Paciente__2C2C72BB85384386");

            entity.ToTable("Paciente");

            entity.Property(e => e.IdPaciente).HasColumnName("id_paciente");
            entity.Property(e => e.Consulta)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("consulta");
            entity.Property(e => e.FechaConsulta)
                .HasColumnType("datetime")
                .HasColumnName("fecha_consulta");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.IdTipoId).HasColumnName("id_tipo_id");
            entity.Property(e => e.NombreCompleto)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre_completo");
            entity.Property(e => e.NumeroId).HasColumnName("numero_id");
            entity.Property(e => e.Status)
                .IsRequired()
                .HasDefaultValueSql("((1))")
                .HasColumnName("status");

            entity.HasOne(d => d.IdTipo).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.IdTipoId)
                .HasConstraintName("FK__Paciente__id_tip__403A8C7D");
        });

        modelBuilder.Entity<TipoIdentificacion>(entity =>
        {
            entity.HasKey(e => e.IdTipoId).HasName("PK__TipoIden__BF3E67570B1ECEC6");

            entity.ToTable("TipoIdentificacion");

            entity.Property(e => e.IdTipoId).HasColumnName("id_tipo_id");
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha_creacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
