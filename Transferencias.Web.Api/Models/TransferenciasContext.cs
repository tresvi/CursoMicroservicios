using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Transferencias.Web.Api.Models;

public partial class TransferenciasContext : DbContext
{
    public TransferenciasContext()
    {
    }

    public TransferenciasContext(DbContextOptions<TransferenciasContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Transferencia> Transferencias { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Transferencias; Integrated Security = True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transferencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Transferencias22");

            entity.Property(e => e.CbuDestino)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.CbuOrigen)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.Concepto)
                .IsRequired()
                .HasMaxLength(64);
            entity.Property(e => e.CuilDestinatario)
                .IsRequired()
                .HasMaxLength(32);
            entity.Property(e => e.CuilOriginante)
                .IsRequired()
                .HasMaxLength(32);
            entity.Property(e => e.Descripcion).HasMaxLength(64);
            entity.Property(e => e.Importe).HasColumnType("decimal(16, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
