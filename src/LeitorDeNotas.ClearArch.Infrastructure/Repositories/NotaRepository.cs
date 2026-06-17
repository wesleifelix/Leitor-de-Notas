using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;

namespace LeitorDeNotas.ClearArch.Infrastructure.Repositories;

public sealed class NotaRepository : INotaRepository
{
    private static readonly List<Nota> _storage = new();

    public Task<IEnumerable<Nota>> ObterTodasAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_storage.AsEnumerable());
    }

    public Task<Nota?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var nota = _storage.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(nota);
    }

    public Task AdicionarAsync(Nota nota, CancellationToken cancellationToken = default)
    {
        _storage.Add(nota);
        return Task.CompletedTask;
    }
}
