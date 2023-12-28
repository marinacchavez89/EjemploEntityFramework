using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace APP_Alumno.Models;

public partial class AlumnoBdContext : DbContext
{
    public AlumnoBdContext()
    {
    }

    public AlumnoBdContext(DbContextOptions<AlumnoBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Alumno> Alumnos { get; set; }

    public virtual DbSet<Carrera> Carreras { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=alumno_bd;user=root;pwd=1234", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.33-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Alumno>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("alumno");

            entity.HasIndex(e => e.CarreraId, "fk_alumno_carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellido)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("apellido");
            entity.Property(e => e.Carrera)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("carrera");
            entity.Property(e => e.CarreraId).HasColumnName("carrera_id");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.FechaNac).HasColumnName("fecha_nac");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("nombre");

            entity.HasOne(d => d.CarreraNavigation).WithMany(p => p.Alumnos)
                .HasForeignKey(d => d.CarreraId)
                .HasConstraintName("fk_alumno_carrera");
        });

        modelBuilder.Entity<Carrera>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("carrera");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnName("nombre");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
