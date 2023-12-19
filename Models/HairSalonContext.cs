using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dominican_Hair_Salon.Models;

public partial class HairSalonContext : DbContext
{
    public HairSalonContext()
    {
    }

    public HairSalonContext(DbContextOptions<HairSalonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventario> Inventarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<RegistroDeVenta> RegistroDeVentas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sucursal> Sucursals { get; set; }

    public virtual DbSet<TicketDeVenta> TicketDeVenta { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-N91JU5J; Database=Hair_Salon;integrated security=true;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Inventario>(entity =>
        {
            entity.ToTable("Inventario");

            entity.Property(e => e.InventarioId).HasColumnName("Inventario_ID");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Product_Name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsFixedLength();
            entity.Property(e => e.SucursalId).HasColumnName("Sucursal_ID");

            entity.HasOne(d => d.Sucursal).WithMany(p => p.Inventarios)
                .HasForeignKey(d => d.SucursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Inventario_Sucursal");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.ServicioId);

            entity.ToTable("Menu");

            entity.Property(e => e.ServicioId).HasColumnName("Servicio_ID");
            entity.Property(e => e.NombreServicio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Nombre_Servicio");
            entity.Property(e => e.Statuts)
                .HasMaxLength(1)
                .IsFixedLength();
        });

        modelBuilder.Entity<RegistroDeVenta>(entity =>
        {
            entity.HasKey(e => e.VentaId);

            entity.ToTable("Registro_de_Ventas");

            entity.Property(e => e.VentaId).HasColumnName("Venta_ID");
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");
            entity.Property(e => e.Total).HasColumnType("numeric(7, 2)");

            entity.HasOne(d => d.Ticket).WithMany(p => p.RegistroDeVenta)
                .HasForeignKey(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Registro_de_Ventas_Ticket_De_Venta");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleName).HasName("PK_Roles_1");

            entity.Property(e => e.RoleName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Role_Name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Sucursal>(entity =>
        {
            entity.HasKey(e => e.SucursalesId);

            entity.ToTable("Sucursal");

            entity.Property(e => e.SucursalesId)
                .ValueGeneratedNever()
                .HasColumnName("Sucursales_ID");
            entity.Property(e => e.Direccion).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<TicketDeVenta>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK_Ticket_de_Venta");

            entity.ToTable("Ticket_De_Venta");

            entity.Property(e => e.TicketId).HasColumnName("Ticket_ID");
            entity.Property(e => e.ClienteNombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cliente_Nombre");
            entity.Property(e => e.Empleada)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Fecha).HasColumnType("date");
            entity.Property(e => e.Precio).HasColumnType("numeric(7, 2)");
            entity.Property(e => e.SurcursalId).HasColumnName("Surcursal_ID");

            entity.HasOne(d => d.Surcursal).WithMany(p => p.TicketDeVenta)
                .HasForeignKey(d => d.SurcursalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Ticket_De_Venta_Sucursal");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId).HasColumnName("User_ID");
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Role)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");

            entity.HasOne(d => d.SucursalNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Sucursal)
                .HasConstraintName("FK_Usuarios_Sucursal");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.TicketId);

            entity.Property(e => e.TicketId)
                .ValueGeneratedNever()
                .HasColumnName("TicketID");
            entity.Property(e => e.ServicioId).HasColumnName("Servicio_ID");

            entity.HasOne(d => d.Servicio).WithMany(p => p.Venta)
                .HasForeignKey(d => d.ServicioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TicketDetails_Menu");

            entity.HasOne(d => d.Ticket).WithOne(p => p.Venta)
                .HasForeignKey<Venta>(d => d.TicketId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Venta_Ticket_De_Venta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
