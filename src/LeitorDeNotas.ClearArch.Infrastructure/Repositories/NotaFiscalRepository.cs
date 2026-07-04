using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;
using LeitorDeNotas.ClearArch.Infrastructure.Data;
using LeitorDeNotas.ClearArch.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace LeitorDeNotas.ClearArch.Infrastructure.Repositories;

public sealed class NotaFiscalRepository : INotaFiscalRepository
{
    private readonly ApplicationDbContext _dbContext;

    public NotaFiscalRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<NotaFiscal> AdicionarAsync(NotaFiscal notaFiscal, CancellationToken cancellationToken = default)
    {
        var entity = new NotaFiscalEntity
        {
            Id = notaFiscal.Id,
            ChaveAcesso = notaFiscal.ChaveAcesso,
            Serie = notaFiscal.Serie,
            DataEmissao = notaFiscal.DataEmissao,
            Tipo = (int)notaFiscal.Tipo,
            ValorTotal = notaFiscal.ValorTotal,
            EstimativaImposto = notaFiscal.EstimativaImposto,
            EstimativaLucroPrejuizo = notaFiscal.EstimativaLucroPrejuizo,
            Itens = notaFiscal.Itens.Select(item => new NotaFiscalItemEntity
            {
                Id = item.Id,
                NotaFiscalId = notaFiscal.Id,
                Descricao = item.Descricao,
                Tipo = item.Tipo,
                Quantidade = item.Quantidade,
                ValorUnitario = item.ValorUnitario,
                ValorTotal = item.ValorTotal
            }).ToList()
        };

        _dbContext.NotasFiscais.Add(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return notaFiscal;
    }

    public async Task<IEnumerable<NotaFiscal>> ObterPorPeriodoAsync(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken = default)
    {
        var entities = await _dbContext.NotasFiscais
            .Include(x => x.Itens)
            .Where(x => x.DataEmissao >= dataInicial && x.DataEmissao <= dataFinal)
            .ToListAsync(cancellationToken);

        return entities.Select(entity => NotaFiscal.Criar(
            entity.ChaveAcesso,
            entity.Serie,
            entity.DataEmissao,
            (NotaFiscal.TipoNota)entity.Tipo,
            entity.Itens.Select(item => NotaFiscalItem.Criar(entity.Id, item.Descricao, item.Tipo, item.Quantidade, item.ValorUnitario, item.NCM, item.SKU)))
        );
    }
}
