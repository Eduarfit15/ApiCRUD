using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Models;

public partial class Prueba1Context : DbContext
{
    public Prueba1Context()
    {
    }

    public Prueba1Context(DbContextOptions<Prueba1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleVentum> DetalleVenta { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Venta> Ventas { get; set; }
    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=sqlservidor1234.database.windows.net;Database=prueba1;User ID=usuarioazure;Password=hola1234&;TrustServerCertificate=True;Encrypt=True;");
    }*/
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.Dni).HasName("PK__Clientes__D87608A687DE9542");

            entity.Property(e => e.Dni)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("dni");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Celular)
                .HasMaxLength(9)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("celular");
            entity.Property(e => e.Correo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Direccion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombres)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nombres");
        });

        modelBuilder.Entity<DetalleVentum>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__DetalleV__BFE2843F77EB475B");

            entity.Property(e => e.IdDetalleVenta).HasColumnName("idDetalleVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdFacturaNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdFactura)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVenta_Ventas");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetalleVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetalleVenta_Productos");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__07F4A1329A6CE6F8");

            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.NomProducto)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("nomProducto");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Stock).HasColumnName("stock");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdFactura).HasName("PK__Ventas__3CD5687E50496456");

            entity.Property(e => e.IdFactura).HasColumnName("idFactura");
            entity.Property(e => e.Cliente)
                .HasMaxLength(8)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("cliente");
            entity.Property(e => e.Fecha).HasColumnName("fecha");
            entity.Property(e => e.ImportFinal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("importFinal");
            entity.Property(e => e.Productos).HasColumnName("productos");

            entity.HasOne(d => d.ClienteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Cliente)
                .HasConstraintName("FK_Ventas_Clientes");

            entity.HasOne(d => d.ProductosNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Productos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Productos");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
