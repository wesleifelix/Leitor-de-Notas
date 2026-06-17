using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Domain.Interfaces;

public interface INotaFiscalRepository
{
    Task<NotaFiscal> AdicionarAsync(NotaFiscal notaFiscal, CancellationToken cancellationToken = default);
    Task<IEnumerable<NotaFiscal>> ObterPorPeriodoAsync(DateTime dataInicial, DateTime dataFinal, CancellationToken cancellationToken = default);
}
