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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Clientes; Integrated Security = True");

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
