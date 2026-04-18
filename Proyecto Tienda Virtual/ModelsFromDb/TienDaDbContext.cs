using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto_Tienda_Virtual.ModelsFromDb;

public partial class TienDaDbContext : DbContext
{
    public TienDaDbContext()
    {
    }

    public TienDaDbContext(DbContextOptions<TienDaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=Estiben_PC;Database=TIENDA_TEC;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.ProductoId).HasName("PK__PRODUCTO__A430AE8346EC8517");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.TransaccionId).HasName("PK__VENTAS__86A849DEC9E820E0");

            entity.HasOne(d => d.Producto).WithMany(p => p.Venta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__VENTAS__Producto__3C69FB99");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
