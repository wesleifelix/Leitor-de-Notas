using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;

namespace LeitorDeNotas.ClearArch.Application.UseCases.Notas;

public sealed class GetNotasQuery
{
    private readonly INotaRepository _notaRepository;

    public GetNotasQuery(INotaRepository notaRepository)
    {
        _notaRepository = notaRepository;
    }

    public Task<IEnumerable<Nota>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return _notaRepository.ObterTodasAsync(cancellationToken);
    }
}
