using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Web.Api.Models;

public partial class ClientesContext : DbContext
{
    public ClientesContext()
    {
    }

    public ClientesContext(DbContextOptions<ClientesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Cliente", "Clientes");

            entity.Property(e => e.Apellido).HasMaxLength(250);
            entity.Property(e => e.Cuil).HasMaxLength(32);
            entity.Property(e => e.EsEmpleadoBna)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))")
                .HasColumnName("EsEmpleadoBNA");
            entity.Property(e => e.Nombre).HasMaxLength(250);
            entity.Property(e => e.PaisOrigen).HasMaxLength(64);
            entity.Property(e => e.TipoDocumento).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
