using LeitorDeNotas.ClearArch.Domain.Entities;

namespace LeitorDeNotas.ClearArch.Domain.Interfaces;

public interface INotaRepository
{
    Task<IEnumerable<Nota>> ObterTodasAsync(CancellationToken cancellationToken = default);
    Task<Nota?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AdicionarAsync(Nota nota, CancellationToken cancellationToken = default);
}
