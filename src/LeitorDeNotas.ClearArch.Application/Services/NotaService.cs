using LeitorDeNotas.ClearArch.Application.Interfaces;
using LeitorDeNotas.ClearArch.Commons;
using LeitorDeNotas.ClearArch.Domain.Entities;
using LeitorDeNotas.ClearArch.Domain.Interfaces;

namespace LeitorDeNotas.ClearArch.Application.Services;

public sealed class NotaService : INotaService
{
    private readonly INotaRepository _notaRepository;

    public NotaService(INotaRepository notaRepository)
    {
        _notaRepository = notaRepository;
    }

    public async Task<OperationResult<IEnumerable<Nota>>> ObterTodasAsync(CancellationToken cancellationToken = default)
    {
        var notas = await _notaRepository.ObterTodasAsync(cancellationToken);
        return OperationResult<IEnumerable<Nota>>.Ok(notas);
    }
}
