using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestionF1.Web.EF;

public partial class Formula1Context : DbContext
{
    public Formula1Context()
    {
    }

    public Formula1Context(DbContextOptions<Formula1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Escuderium> Escuderia { get; set; }

    public virtual DbSet<Piloto> Pilotos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-CGLP4AL\\SQLEXPRESS;Database=Formula1;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Escuderium>(entity =>
        {
            entity.HasKey(e => e.IdEscuderia).HasName("PK__Escuderi__B1F79A4639531EF2");

            entity.Property(e => e.IdEscuderia)
                .ValueGeneratedNever()
                .HasColumnName("id_escuderia");
            entity.Property(e => e.NombreEscuderia)
                .HasMaxLength(50)
                .HasColumnName("nombre_escuderia");
        });

        modelBuilder.Entity<Piloto>(entity =>
        {
            entity.HasKey(e => e.IdPiloto).HasName("PK__Piloto__93ED523531424F99");

            entity.ToTable("Piloto");

            entity.Property(e => e.IdPiloto)
                .ValueGeneratedOnAdd()
                .HasColumnName("id_piloto");
            entity.Property(e => e.Eliminado).HasColumnName("eliminado");
            entity.Property(e => e.IdEscuderia).HasColumnName("id_escuderia");
            entity.Property(e => e.NombrePiloto)
                .HasMaxLength(50)
                .HasColumnName("nombre_piloto");

            entity.HasOne(d => d.IdEscuderiaNavigation).WithMany(p => p.Pilotos)
                .HasForeignKey(d => d.IdEscuderia)
                .HasConstraintName("FK__Piloto__id_escud__1367E606");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
