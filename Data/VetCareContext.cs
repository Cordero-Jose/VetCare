using Microsoft.EntityFrameworkCore;
using VetCare.Models;

namespace VetCare.Data;

public partial class VetCareContext : DbContext
{
    public VetCareContext(DbContextOptions<VetCareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Mascota> Mascotas { get; set; }
    public virtual DbSet<Consulta> Consultas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Mascota>(entity =>
        {
            entity.HasKey(e => e.IdMascota).HasName("PK_Mascotas");

            entity.ToTable("Mascotas");

            entity.Property(e => e.Nombre).HasMaxLength(100);
            entity.Property(e => e.Especie).HasMaxLength(50);
            entity.Property(e => e.Raza).HasMaxLength(100);
            entity.Property(e => e.NombrePropietario).HasMaxLength(150);
            entity.Property(e => e.TelefonoPropietario).HasMaxLength(20);
        });

        modelBuilder.Entity<Consulta>(entity =>
        {
            entity.HasKey(e => e.IdConsulta).HasName("PK_Consultas");

            entity.ToTable("Consultas");

            entity.Property(e => e.FechaConsulta).HasColumnType("datetime2(0)");
            entity.Property(e => e.Motivo).HasMaxLength(250);
            entity.Property(e => e.Diagnostico).HasMaxLength(500);
            entity.Property(e => e.Tratamiento).HasMaxLength(500);

            entity.HasOne(e => e.Mascota)
                  .WithMany(m => m.Consultas)
                  .HasForeignKey(e => e.IdMascota)
                  .HasConstraintName("FK_Consultas_Mascotas")
                  .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
