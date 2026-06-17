using LeitorDeNotas.ClearArch.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeitorDeNotas.ClearArch.Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<NotaFiscalEntity> NotasFiscais { get; set; } = null!;
    public DbSet<NotaFiscalItemEntity> NotaFiscalItens { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NotaFiscalEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.ChaveAcesso).IsRequired();
            entity.Property(x => x.Serie).IsRequired();
            entity.Property(x => x.Tipo).IsRequired();
            entity.HasMany(x => x.Itens)
                  .WithOne(x => x.NotaFiscal!)
                  .HasForeignKey(x => x.NotaFiscalId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<NotaFiscalItemEntity>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Descricao).IsRequired();
            entity.Property(x => x.Tipo).IsRequired();
        });

        base.OnModelCreating(modelBuilder);
    }
}
